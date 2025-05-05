using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class PlayerRoundResult : BaseEntity
{
    [Required] [MaxLength(MaxIdLength)] public int PlayerId { get; set; }

    [Required] public Player Player { get; set; } = null!;

    [Required] [MaxLength(MaxIdLength)] public int MatchRoundId { get; set; }

    [Required] public MatchRound MatchRound { get; set; } = null!;

    [Range(0, int.MaxValue)] public int Points { get; set; }

    [Required] public int Won { get; set; }

    [Required] public int Lost { get; set; }

    [Required] public int Table { get; set; }
}