using Microsoft.AspNetCore.Mvc;
using skat_back.DTO.UserDTO;
using skat_back.services.UserService;

namespace skat_back.controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<UserResponseDto> users = await service.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto user)
    {
        var newUser = await service.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserDto user)
    {
        var updated = await service.UpdateUserAsync(id, user);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await service.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}