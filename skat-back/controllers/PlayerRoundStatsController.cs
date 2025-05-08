using skat_back.dto.PlayerRoundResultDto;
using skat_back.services.PlayerRoundResultsService;

namespace skat_back.controllers;

public class PlayerRoundStatsController(IPlayerRoundResultService service)
    : BaseController<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto, UpdatePlayerRoundStatsDto, int,
        IPlayerRoundResultService>(
        service);