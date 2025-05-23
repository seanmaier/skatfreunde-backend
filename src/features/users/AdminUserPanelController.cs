using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.auth.models;
using skat_back.features.email;

namespace skat_back.features.users;

[ApiController]
[Authorize(Roles = "Admin,Manager")]
[Route("api/admin/users")]
public class AdminUserPanelController(
    UserManager<ApplicationUser> userManager,
    ILogger<AdminUserPanelController> logger,
    IEmailService emailService) : ControllerBase
{
    [HttpPost("approve/{userId}")]
    public async Task<IActionResult> ApproveUser(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user?.Email is null)
        {
            logger.LogWarning("User could not be found while trying to approve");
            return NotFound("User not found");
        }

        user.IsApproved = true;
        await userManager.AddToRoleAsync(user, "User");

        await userManager.UpdateAsync(user);

        await emailService.SendEmailAsync(user.Email, "Account Approved",
            "Your account has been approved. You can now log in.");

        return Ok("User approved");
    }
}