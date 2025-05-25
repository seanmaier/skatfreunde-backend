using skat_back.features.playerRoundStatistics.models;

namespace skat_back.features.matchRounds.models;

/// <summary>
///     Represents the data transfer object (DTO) for a match round.
/// </summary>
public sealed record ResponseMatchRoundDto(
    int Id,
    int MatchSessionId,
    string RoundNumber,
    string Table,
    ICollection<ResponsePlayerRoundStatsDto> PlayerRoundStats,
    DateTime CreatedAt,
    DateTime UpdatedAt
);