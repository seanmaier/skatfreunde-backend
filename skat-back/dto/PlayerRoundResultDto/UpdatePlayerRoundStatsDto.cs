namespace skat_back.dto.PlayerRoundResultDto;

public sealed record UpdatePlayerRoundStatsDto(
    int PlayerId,
    int Points,
    int Won,
    int Lost,
    string Table
);