using Microsoft.AspNetCore.Mvc;
using skat_back.models;
using skat_back.services.UserService;

namespace skat_back.controllers;

[ApiController]
[Route("api/users")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        IEnumerable<User> users = await service.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        User? user = await service.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        User newUser = await service.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserById), new {id = newUser.Id}, newUser);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(string id, User user)
    {
        bool updated = await service.UpdateUserAsync(id, user);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUser(string id)
    {
        bool deleted = await service.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}