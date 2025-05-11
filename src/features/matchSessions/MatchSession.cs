using System.ComponentModel.DataAnnotations;
using skat_back.Features.MatchRounds;
using skat_back.Features.Users;
using skat_back.models;

namespace skat_back.Features.MatchSessions;

/// <summary>
///     Represents a match session entity for the Database.
/// </summary>
public class MatchSession : BaseEntity
{
    [Key] public int Id { get; set; }

    public string CalendarWeek { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public Guid CreatedByUserId { get; set; }

    public User CreatedByUser { get; set; } = null!;

    public ICollection<MatchRound> MatchRounds { get; set; } = new HashSet<MatchRound>();
}