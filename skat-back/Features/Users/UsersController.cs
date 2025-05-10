using Microsoft.AspNetCore.Mvc;
using skat_back.controllers;

namespace skat_back.Features.Users;

/// <summary>
///     Represents the API controller for managing users.
/// </summary>
/// <param name="service">The injected User Service</param>
public class UsersController(IUserService service)
    : BaseController<ResponseUserDto, CreateUserDto, UpdateUserDto, Guid, IUserService>(service)
{
    private readonly IUserService _service = service;

    [HttpGet("checkEmail")]
    public async Task<IActionResult> CheckEmail(string email)
    {
        var result = await _service.CheckEmailAsync(email);
        return Ok(result);
    }
}