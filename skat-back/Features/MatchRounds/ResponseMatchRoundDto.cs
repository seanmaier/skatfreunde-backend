using skat_back.Features.PlayerRoundStatistics;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the data transfer object (DTO) for a match round.
/// </summary>
public sealed record ResponseMatchRoundDto(
    int Id,
    int MatchSessionId,
    string RoundNumber,
    ICollection<ResponsePlayerRoundStatsDto> PlayerRoundStats,
    DateTime CreatedAt,
    DateTime UpdatedAt
);