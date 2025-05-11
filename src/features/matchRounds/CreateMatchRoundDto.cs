using skat_back.Features.PlayerRoundStatistics;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the data transfer object (DTO) for creating a match round.
/// </summary>
/// <param name="RoundNumber">The match round number</param>
/// <param name="PlayerRoundStats">A collection of statistics of each player per round</param>
public sealed record CreateMatchRoundDto(
    string RoundNumber,
    ICollection<CreatePlayerRoundStatsDto> PlayerRoundStats
);