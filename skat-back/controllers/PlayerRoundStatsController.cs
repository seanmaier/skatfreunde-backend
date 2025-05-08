using skat_back.dto.PlayerRoundResultDto;
using skat_back.services.PlayerRoundResultsService;

namespace skat_back.controllers;

public class PlayerRoundStatsController(IPlayerRoundStatsService service)
    : BaseController<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto, UpdatePlayerRoundStatsDto, int,
        IPlayerRoundStatsService>(
        service);