using skat_back.Features.PlayerRoundStatistics;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the data transfer object (DTO) for updating a match round.
/// </summary>
/// <param name="RoundNumber"></param>
/// <param name="PlayerRoundStats">A collection of statistics of each player per round</param>
public sealed record UpdateMatchRoundDto(
    string RoundNumber,
    string Table,
    ICollection<UpdatePlayerRoundStatsDto> PlayerRoundStats
);