using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.dto.MatchRoundDto;

public sealed record ResponseMatchRoundDto
{
    public string Id { get; init; } = string.Empty;
    public string RoundNumber { get; init; } = string.Empty;
    public string MatchSessionId { get; init; } = string.Empty;
    public ICollection<ResponsePlayerRoundStatsDto> PlayerRoundResults { get; init; } = new List<ResponsePlayerRoundStatsDto>();
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}