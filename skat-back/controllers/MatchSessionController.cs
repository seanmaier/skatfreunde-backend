using skat_back.models;
using skat_back.services.MatchSessionService;

namespace skat_back.controllers;

public class MatchSessionController(MatchSessionService service)
    : BaseController<MatchSession, MatchSessionService>(service);