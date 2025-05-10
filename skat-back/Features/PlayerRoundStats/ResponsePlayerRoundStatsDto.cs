namespace skat_back.dto.PlayerRoundResultDto;

public sealed record ResponsePlayerRoundStatsDto(
    int PlayerId,
    int MatchRoundId,
    int Points,
    int Won,
    int Lost,
    string Table,
    DateTime CreatedAt,
    DateTime UpdatedAt
);