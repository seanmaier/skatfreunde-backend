using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.dto.MatchRoundDto;

public sealed record ResponseMatchRoundDto(
    int Id,
    int MatchSessionId,
    string RoundNumber,
    ICollection<ResponsePlayerRoundStatsDto> PlayerRoundStats,
    DateTime CreatedAt,
    DateTime UpdatedAt
);