using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.auth.models;
using skat_back.utilities.mapping;

namespace skat_back.features.auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ILogger<AuthController> logger,
    TokenService tokenService)
    : ControllerBase
{
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
        ApplicationUser? user = null;

        // 1. Check if the input is an email or username
        if (IsValidEmail(dto.LoginInput))
            user = await userManager.FindByEmailAsync(dto.LoginInput);
        else
            user = await userManager.FindByNameAsync(dto.LoginInput);

        // 2. Reject if user not found or credentials are invalid
        if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
        {
            logger.LogError("Invalid login attempt for {Input}", dto.LoginInput);
            return Unauthorized();
        }

        // 3. Check if the user is locked out
        if (await userManager.IsLockedOutAsync(user))
        {
            logger.LogWarning("User {UserId} account locked out.", user.Id);
            return StatusCode(423, "Account locked out.");
        }

        // 4. Reject if Email/Username is null
        if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.UserName))
        {
            logger.LogError("User {UserId} has no email or username.", user.Id);
            return BadRequest("Email or Username is missing");
        }


        // 5. Generate JWT token
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = tokenService.GenerateToken(user, claims);
        
        return Ok(new { success = true, token });
    }


    [HttpPost("logout")]
    public async Task<IActionResult> LogOut([FromBody] object? empty)
    {
        try
        {
            await signInManager.SignOutAsync();
            logger.LogInformation("User logged out.");
            Response.Cookies.Delete("authToken");
            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error logging out user.");
            return StatusCode(500, "Error during logout");
        }
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
}