using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.user.models;

namespace skat_back.features.user;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await userService.GetUserByIdAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }
    
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var user = await userService.CreateUserAsync(createUserDto);
        return CreatedAtAction(nameof(GetUserById), new { id = (user as dynamic).Id }, user);
    }
    
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await userService.UpdateUserAsync(userId, updateUserDto);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await userService.DeleteUserAsync(userId);
        return result ? NoContent() : NotFound();
    }
}