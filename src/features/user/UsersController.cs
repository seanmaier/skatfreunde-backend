using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.user.models;

namespace skat_back.features.user;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService, ILogger<UsersController> logger) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await usersService.GetAllUsersAsync();
        return Ok(users);
    }
    
    /// <summary>
    /// Endpoint for admins and managers to get a user by their ID. 
    /// </summary>
    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await usersService.GetUserByIdAsync(userId);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetOwnUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId is null)
        {
            logger.LogWarning("User not found.");
            return Unauthorized();
        }

        var user = await usersService.GetUserByIdAsync(userId);
        return Ok(user);
    }
    
    
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        var user = await usersService.CreateUserAsync(createUserDto);
        return CreatedAtAction(nameof(GetUserById), new { id = (user as dynamic).Id }, user);
    }
    
    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
    {
        var result = await usersService.UpdateUserAsync(userId, updateUserDto);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var result = await usersService.DeleteUserAsync(userId);
        return result ? NoContent() : NotFound();
    }
}