using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.auth.models;
using skat_back.utilities.mapping;
using static skat_back.utilities.constants.GeneralConstants;

namespace skat_back.features.auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    ILogger<AuthController> logger,
    TokenService tokenService,
    AppDbContext context)
    : ControllerBase
{
    private const int ExpirationTime = 15;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = dto.ToEntity();

        var result = await userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "User");
            logger.LogInformation("User created a new account with password.");
            return Ok(new { Message = "User created successfully" });
        }

        logger.LogError("User creation failed: {Errors}", result.Errors);
        return BadRequest(result.Errors);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        // 1. Check if the input is an email or username
        var user = IsValidEmail(dto.LoginInput)
            ? await userManager.FindByEmailAsync(dto.LoginInput)
            : await userManager.FindByNameAsync(dto.LoginInput);

        if (user is null)
        {
            logger.LogError("User not found: {Input}", dto.LoginInput);
            return Unauthorized("Invalid login attempt");
        }

        // 2. Check if the user is locked out
        if (await userManager.IsLockedOutAsync(user))
        {
            logger.LogWarning("User {UserId} account locked out.", user.Id);
            return StatusCode(423, "Account locked out.");
        }

        // Reject if user credentials are invalid
        if (!await userManager.CheckPasswordAsync(user, dto.Password))
        {
            logger.LogError("Invalid login attempt for {Input}", dto.LoginInput);
            return Unauthorized();
        }

        // Mark and revoke old refresh tokens
        await context.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(); // Without this, RefreshTokens is zero

        foreach (var group in user.RefreshTokens.Where(t => t.IsActive).GroupBy(t => t.UserAgent))
        {
            var tokensToKeep = group.OrderByDescending(t => t.Created).Take(4).ToList();

            var tokensToDelete = group.Except(tokensToKeep).ToList();
            foreach (var token in tokensToDelete) tokenService.RevokeToken(token);
        }

        // Delete expired tokens
        user.RefreshTokens.RemoveAll(token => !token.IsActive);

        // 5. Generate tokens
        var userClaims = await GetUserClaims(user);

        var accessToken = tokenService.GenerateJwtToken(user.UserName ?? "unknown", userClaims, ExpirationTime);

        var remembered = dto.RememberMe ? 30 : 1;
        var refreshExpiration = DateTime.UtcNow.AddDays(remembered);
        var refreshToken = tokenService.GenerateRefreshToken(refreshExpiration, user.Id);

        // 6. Save refresh token to database
        context.RefreshTokens.Add(refreshToken);
        var affectedRows = await context.SaveChangesAsync();

        // 7. Check if the update was successful
        if (affectedRows == 0)
        {
            logger.LogError("Failed to update user with refresh token");
            return StatusCode(500, "An internal error occurred");
        }

        // 8. Set the tokens in the cookie
        SetTokenCookies(accessToken, refreshToken.Token, refreshToken.Expires);

        return Ok(new { success = true });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> LogOut()
    {
        // 1. Check for the user ID in the claim
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            logger.LogWarning("User not found during logout.");
            return Unauthorized();
        }

        // 2. Check for the refresh token in the cookie
        if (!Request.Cookies.TryGetValue(RefreshTokenKey, out var refreshToken))
        {
            logger.LogWarning("No refresh token found in cookies.");
            return BadRequest("Refresh token not found");
        }

        // 3. Find the user and the refresh token in the database
        var user = await userManager.FindByIdAsync(userId);
        if (user is null)
        {
            logger.LogWarning("User not found in database.");
            return BadRequest("User not found");
        }

        // 4. Revoke the old refresh token
        await context.Entry(user).Collection(u => u.RefreshTokens).LoadAsync(); // Without this, RefreshTokens is zero
        var token = user.RefreshTokens.FirstOrDefault(rft => rft.Token == refreshToken);
        if (token is null)
        {
            logger.LogWarning("Refresh token not found in database.");
            return BadRequest("Refresh token not found");
        }

        tokenService.RevokeToken(token);

        // 5. Update the token in the database
        await context.SaveChangesAsync();
        logger.LogInformation("User logged out.");

        // 6. Clear the cookies
        SetTokenCookies("", "", DateTime.UnixEpoch);

        return Ok();
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        // 1. Extract refresh token from cookie
        if (!Request.Cookies.TryGetValue(RefreshTokenKey, out var requestRefreshToken))
        {
            logger.LogWarning("No refresh token cookie present");
            return Unauthorized("Refresh token missing");
        }

        // 2. Find the user with the refresh token
        var user = await userManager.Users
            .Include(u => u.RefreshTokens)
            .SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == requestRefreshToken));

        if (user is null)
        {
            logger.LogWarning("No user found for refresh token");
            return Unauthorized("Invalid refresh token");
        }

        var oldRefreshToken = user.RefreshTokens.Single(rft => rft.Token == requestRefreshToken);

        // 3. Validate token is active
        if (!oldRefreshToken.IsActive)
        {
            logger.LogWarning("Expired or revoked refresh token used");
            return Unauthorized("Refresh token is no longer valid");
        }

        // 4. Rotate tokens
        var userClaims = await GetUserClaims(user);
        var (newAccessToken, newRefreshToken) = tokenService.RotateTokens(user, oldRefreshToken, userClaims);
        context.RefreshTokens.Remove(oldRefreshToken);

        // 5. Save to database
        context.RefreshTokens.Add(newRefreshToken);
        var affectedRows = await context.SaveChangesAsync();

        if (affectedRows == 0)
        {
            logger.LogError("Failed to update user with refresh token");
            return StatusCode(500, "An internal error occurred");
        }

        // 7. Set new cookies
        SetTokenCookies(newAccessToken, newRefreshToken.Token, oldRefreshToken.Expires);

        logger.LogInformation("Refresh token rotated for user {UserId}", user.Id);
        return Ok("Token refreshed");
    }

    private static bool IsValidEmail(string email)
    {
        var trimmedEmail = email.Trim();

        if (trimmedEmail.EndsWith(".")) return false;
        try
        {
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }

    private void SetTokenCookies(string accessToken, string refreshToken, DateTime refreshExpiration)
    {
        HttpContext.Response.Cookies.Append(AccessTokenKey, accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.Now.AddMinutes(ExpirationTime)
        });

        HttpContext.Response.Cookies.Append(RefreshTokenKey, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = refreshExpiration
        });
    }

    private async Task<List<Claim>> GetUserClaims(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.NameId, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        }.Concat(roles.Select(role => new Claim(ClaimTypes.Role, role))).ToList();

        return claims;
    }
}