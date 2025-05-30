using System.ComponentModel.DataAnnotations;
using skat_back.features.matches.matchSessions.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.Lib;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.features.matches.matchRounds.models;

/// <summary>
///     Represents a match round entity for the Database.
/// </summary>
public class MatchRound : IEntity
{
    [MaxLength(MaxRoundNumberLength)] public required string RoundNumber { get; set; }

    [MaxLength(2)] public required string Table { get; set; } = null!;

    /*--------------------Navigation  Properties--------------------*/

    [Required] public int MatchSessionId { get; set; }
    public MatchSession MatchSession { get; set; } = null!;

    public ICollection<PlayerRoundStats> PlayerRoundStats { get; set; } = new HashSet<PlayerRoundStats>();
    [Key] public int Id { get; set; }

    /*------------------------Updater Logic------------------------*/

    public void UpdateFrom(MatchRound matchRound)
    {
        RoundNumber = matchRound.RoundNumber;
        Table = matchRound.Table;
    }
}