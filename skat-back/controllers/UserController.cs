using skat_back.models;
using skat_back.services.UserService;

namespace skat_back.controllers;

public class UserController(UserService service) : BaseController<User, UserService>(service);