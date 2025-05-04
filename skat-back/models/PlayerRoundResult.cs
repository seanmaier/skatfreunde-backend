using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class PlayerRoundResult
{
    [Required] [MaxLength(MaxIdLength)] public string PlayerId { get; set; } = null!;

    [Required] public Player Player { get; set; } = null!;

    [Required] [MaxLength(MaxIdLength)] public string MatchRoundId { get; set; } = null!;

    [Required] public MatchRound MatchRound { get; set; } = null!;

    [Range(0, int.MaxValue)] public int Points { get; set; }

    [Required] public int Won { get; set; }

    [Required] public int Lost { get; set; }

    [Required] public int Table { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}