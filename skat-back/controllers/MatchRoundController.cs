using skat_back.models;
using skat_back.services.MatchRoundService;

namespace skat_back.controllers;

public class MatchRoundController(MatchRoundRoundService service)
    : BaseController<MatchRound, MatchRoundRoundService>(service);