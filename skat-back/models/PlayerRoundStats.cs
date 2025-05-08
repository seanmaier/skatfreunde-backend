using System.ComponentModel.DataAnnotations;

namespace skat_back.models;

public class PlayerRoundStats : BaseEntity
{
    [Range(0, int.MaxValue)] public int Points { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }
    public string Table { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Required] public int MatchRoundId { get; set; }
    public MatchRound MatchRound { get; set; } = null!;
}