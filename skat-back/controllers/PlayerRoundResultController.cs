using skat_back.models;
using skat_back.services.PlayerRoundResultsService;

namespace skat_back.controllers;

public class PlayerRoundResultController(PlayerRoundResultService service)
    : BaseController<PlayerRoundResult, PlayerRoundResultService>(service);