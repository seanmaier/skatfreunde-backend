using skat_back.DTO.UserDTO;
using skat_back.services.UserService;

namespace skat_back.controllers;

public class UsersController(IUserService service)
    : BaseController<ResponseUserDto, CreateUserDto, UpdateUserDto, Guid, IUserService>(service)
{
}