namespace skat_back.dto.PlayerRoundResultDto;

public sealed record CreatePlayerRoundStatsDto(
    int PlayerId,
    int MatchRoundId,
    int Points,
    int Won,
    int Lost,
    string Table
);