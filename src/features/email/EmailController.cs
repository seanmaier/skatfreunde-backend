using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.auth.models;
using skat_back.features.email.models;

namespace skat_back.features.email;

[ApiController]
[Route("api/[controller]")]
public class EmailController(
    UserManager<ApplicationUser> userManager,
    ILogger<EmailController> logger,
    IEmailService emailService)
    : ControllerBase
{
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ForgotPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user is null || !await userManager.IsEmailConfirmedAsync(user))
        {
            logger.LogWarning("User {User} not found or email not confirmed for password reset", user?.UserName);
            return Ok();
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink =
            $"{dto.FrontendUrl}?email={Uri.EscapeDataString(dto.Email)}&token={Uri.EscapeDataString(token)}";

        await emailService.SendEmailAsync(dto.Email, "Reset your password",
            $"Click here to reset your password: <a href='{resetLink}'>Reset Password</a>");

        logger.LogInformation("Password reset link sent to {Email}", dto.Email);
        return Ok("Password reset link sent");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ResetPasswordDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email);
        if (user is null)
        {
            logger.LogWarning("User not found for password reset");
            return BadRequest("User not found");
        }

        var result = await userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        if (result.Succeeded) return Ok("Password has been reset.");

        logger.LogError("Password reset failed: {Errors}", result.Errors);
        return BadRequest(result.Errors);
    }

    [HttpPost("test-email")]
    public async Task<IActionResult> SendTestEmail()
    {
        await emailService.SendEmailAsync("receiver@example.com", "Test Subject", "Hello from MailKit!");
        return Ok("Email sent.");
    }
}