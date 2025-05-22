using System.ComponentModel.DataAnnotations;
using skat_back.Features.MatchRounds;
using skat_back.Features.Players;
using skat_back.models;

namespace skat_back.Features.PlayerRoundStatistics;

/// <summary>
///     Represents a player round statistics entity for the Database.
/// </summary>
public class PlayerRoundStats : BaseEntity
{
    [Range(0, int.MaxValue)] public required int Points { get; set; }
    public required int Won { get; set; }
    public required int Lost { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Required] public int MatchRoundId { get; set; }
    public MatchRound MatchRound { get; set; } = null!;
}