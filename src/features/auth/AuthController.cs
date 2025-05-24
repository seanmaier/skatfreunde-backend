using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skat_back.data;
using skat_back.features.auth.models;
using static skat_back.utilities.constants.GeneralConstants;

namespace skat_back.features.auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    ILogger<AuthController> logger,
    IAuthService authService,
    ITokenService tokenService,
    AppDbContext context)
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await authService.RegisterAsync(dto);

        if (result.Succeeded)
            return Ok(new { Message = "User created successfully. Confirm your account in the next 24 hours." });

        logger.LogError("User registration failed: {Errors}", result.Errors);
        return BadRequest(result.Errors);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await authService.LoginAsync(dto);
        SetTokenCookies(result.AccessToken, result.RefreshToken.Token, result.RefreshToken.Expires);
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

        await authService.LogoutAsync(userId, refreshToken);

        // Clear the cookies
        SetTokenCookies("", "", DateTime.UnixEpoch);

        return Ok("Logged out successfully");
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        // Extract refresh token from cookie
        if (!Request.Cookies.TryGetValue(RefreshTokenKey, out var requestRefreshToken))
        {
            logger.LogWarning("No refresh token cookie present");
            return Unauthorized("Refresh token missing");
        }

        var result = await authService.RefreshTokenAsync(requestRefreshToken);

        // Set new cookies
        SetTokenCookies(result.AccessToken, result.RefreshToken, result.ExpiresAt);

        return Ok("Token refreshed");
    }


    private void SetTokenCookies(string accessToken, string refreshToken, DateTime refreshTokenExpiration)
    {
        HttpContext.Response.Cookies.Append(AccessTokenKey, accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTime.Now.AddMinutes(TokenExpirationTime)
        });

        HttpContext.Response.Cookies.Append(RefreshTokenKey, refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = refreshTokenExpiration
        });
    }
}