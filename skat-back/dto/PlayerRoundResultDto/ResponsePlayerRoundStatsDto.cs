namespace skat_back.dto.PlayerRoundResultDto;

public sealed record ResponsePlayerRoundStatsDto
{
    public string PlayerId { get; init; }
    public int Points { get; init; }
    public int Won { get; init; }
    public int Lost { get; init; }
    public string Table { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}