using skat_back.features.matches.playerRoundStatistics.models;

namespace skat_back.features.matches.matchRounds.models;

/// <summary>
///     Represents the data transfer object (DTO) for creating a match round.
/// </summary>
/// <param name="RoundNumber">The match round number</param>
/// <param name="PlayerRoundStats">A collection of statistics of each player per round</param>
public sealed record CreateMatchRoundDto(
    string RoundNumber,
    string Table,
    ICollection<CreatePlayerRoundStatsDto> PlayerRoundStats
);