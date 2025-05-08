using System.ComponentModel.DataAnnotations;

namespace skat_back.models;

public class PlayerRoundStats : BaseEntity
{
    [Range(0, int.MaxValue)] public required int Points { get; set; }
    public required int Won { get; set; }
    public required int Lost { get; set; }
    public required string Table { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Required] public required int MatchRoundId { get; set; }
    public MatchRound MatchRound { get; set; } = null!;
}