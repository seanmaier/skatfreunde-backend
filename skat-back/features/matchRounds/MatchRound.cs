using System.ComponentModel.DataAnnotations;
using skat_back.Features.MatchSessions;
using skat_back.Features.PlayerRoundStatistics;
using skat_back.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents a match round entity for the Database.
/// </summary>
public class MatchRound : BaseEntity
{
    [Key] public int Id { get; set; }

    [MaxLength(MaxCharLength)] public string RoundNumber { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public int MatchSessionId { get; set; }
    public MatchSession MatchSession { get; set; } = null!;

    public ICollection<PlayerRoundStats> PlayerRoundStats { get; set; } = new HashSet<PlayerRoundStats>();
}