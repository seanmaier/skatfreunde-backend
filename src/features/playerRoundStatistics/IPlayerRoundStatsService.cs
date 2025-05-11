using skat_back.services;

namespace skat_back.Features.PlayerRoundStatistics;

/// <summary>
///     Represents the service interface for managing player round statistics.
///     Specific implementations for the PlayerRoundStats Service should be provided here.
/// </summary>
public interface IPlayerRoundStatsService : IService<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto,
    UpdatePlayerRoundStatsDto, int>
{
}