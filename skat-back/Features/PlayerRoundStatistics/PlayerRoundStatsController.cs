using skat_back.controllers;

namespace skat_back.Features.PlayerRoundStatistics;

/// <summary>
///     Represents the API controller for managing player round statistics.
/// </summary>
/// <param name="service">The injected PlayerRoundStats service</param>
public class PlayerRoundStatsController(IPlayerRoundStatsService service)
    : BaseController<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto, UpdatePlayerRoundStatsDto, int,
        IPlayerRoundStatsService>(
        service);