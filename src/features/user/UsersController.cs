using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skat_back.features.user.models;
using skat_back.utilities.exceptions;

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
        logger.LogInformation("Fetching all users");

        var users = await usersService.GetAllUsersAsync();
        logger.LogInformation("Fetched {Count} users", users.Count);

        return Ok(users);
    }

    /// <summary>
    ///     Endpoint for admins and managers to get a user by their ID.
    /// </summary>
    [HttpGet("{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await usersService.GetUserByIdAsync(userId);

        logger.LogInformation("Fetching user with ID: {UserId}", userId);
        if (user is null)
            throw new ResourceNotFoundException("User", userId);

        logger.LogInformation("Fetched user: {UserName}", user.Username);
        return Ok(user);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetOwnUser()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        logger.LogInformation("Fetching current user with ID: {UserId}", userId);

        if (userId is null)
            throw new ResourceNotFoundException("User", "Current user");

        var user = await usersService.GetUserByIdAsync(userId);
        logger.LogInformation("Fetched current user: {UserName}", user?.Username ?? "Unknown");

        return Ok(user);
    }


    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
    {
        logger.LogInformation("Creating user with email: {Email}", createUserDto.Email);
        if (await usersService.EmailExistsAsync(createUserDto.Email))
            throw new BusinessLogicException("Email already in use", "EMAIL_IN_USE");

        var user = await usersService.CreateUserAsync(createUserDto);
        logger.LogInformation("Created user with ID: {UserId}", user.Id);
        return CreatedAtAction(nameof(GetUserById), new { id = (user as dynamic).Id }, user);
    }

    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(string userId, [FromBody] UpdateUserDto updateUserDto)
    {
        logger.LogInformation("Updating user with ID: {UserId}", userId);
        var result = await usersService.UpdateUserAsync(userId, updateUserDto);
        return result ? NoContent() : NotFound();
    }

    [HttpPost("{userId}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        logger.LogInformation("Deleting user with ID: {UserId}", userId);
        var result = await usersService.DeleteUserAsync(userId);

        if (!result) return NotFound();

        logger.LogInformation("Deleted user with ID: {UserId}", userId);
        return NoContent();
    }
}