using System.ComponentModel.DataAnnotations;

namespace skat_back.dto.PlayerRoundResultDto;

public sealed record CreatePlayerRoundStatsDto(
    int PlayerId,
    int Points,
    int Won,
    int Lost,
    string Table
);