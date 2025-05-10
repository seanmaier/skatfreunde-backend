using Microsoft.AspNetCore.Mvc;
using skat_back.DTO.UserDTO;
using skat_back.services.UserService;

namespace skat_back.controllers;

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