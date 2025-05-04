using skat_back.models;
using skat_back.services.MatchRoundService;

namespace skat_back.controllers;

public class MatchRoundController(MatchRoundService service)
    : BaseController<MatchRound, MatchRoundService>(service);