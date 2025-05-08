using skat_back.dto.MatchSessionDto;
using skat_back.models;
using skat_back.services.MatchSessionService;

namespace skat_back.controllers;

public class MatchSessionsController(IMatchSessionService service)
    : BaseController<ResponseMatchSessionDto, CreateMatchSessionDto, UpdateMatchSessionDto, int, IMatchSessionService>(service);