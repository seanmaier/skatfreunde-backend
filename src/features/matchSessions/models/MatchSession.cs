using System.ComponentModel.DataAnnotations;
using skat_back.features.auth.models;
using skat_back.features.matchRounds.models;
using skat_back.Lib;

namespace skat_back.features.matchSessions.models;

/// <summary>
///     Represents a match session entity for the Database.
/// </summary>
public class MatchSession : BaseEntity
{
    [Key] public int Id { get; set; }

    [MaxLength(4)] public required string CalendarWeek { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required Guid CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;

    public ICollection<MatchRound> MatchRounds { get; set; }
}