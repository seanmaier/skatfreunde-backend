using System.ComponentModel.DataAnnotations;
using skat_back.features.auth.models;
using skat_back.Features.MatchRounds;
using skat_back.features.matchRounds.models;
using skat_back.models;

namespace skat_back.features.matchSessions.models;

/// <summary>
///     Represents a match session entity for the Database.
/// </summary>
public class MatchSession : BaseEntity
{
    [Key] public int Id { get; set; }

    [MaxLength(4)]
    public required string CalendarWeek { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public Guid CreatedByUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;

    public ICollection<MatchRound> MatchRounds { get; set; } = new HashSet<MatchRound>();
}