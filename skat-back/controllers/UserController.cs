using skat_back.data;
using skat_back.services.UserService;

namespace skat_back.controllers;

public class UserController: BaseController<User, UserService>
{
    public UserController(UserService service) : base(service){}
}