using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.dto.MatchRoundDto;

public sealed record UpdateMatchRoundDto(
    string RoundNumber,
    ICollection<UpdatePlayerRoundStatsDto> PlayerRoundStats
);