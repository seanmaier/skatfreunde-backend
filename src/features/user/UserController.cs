using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.user.models;

namespace skat_back.features.user;

[Authorize]
[ApiController]
[Route("api/[controller]/{userId}")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await userService.GetUser(userId);

        return user is null ? NotFound() : Ok(user);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await userService.UpdateUser(userId, updateUserDto);
        return result ? NoContent() : NotFound();
    }

    [HttpPut]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await userService.DeleteUser(userId);
        return result ? NoContent() : NotFound();
    }
}