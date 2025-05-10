using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.dto.MatchRoundDto;

public sealed record CreateMatchRoundDto(
    string RoundNumber,
    ICollection<CreatePlayerRoundStatsDto> PlayerRoundResults
);