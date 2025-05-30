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
    [MaxLength(4)] public required string CalendarWeek { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    public ICollection<MatchRound> MatchRounds { get; set; } = new HashSet<MatchRound>();
    
    /*------------------------Updater Logic------------------------*/
    public void UpdateFrom(MatchSession matchSession)
    {
        CalendarWeek = matchSession.CalendarWeek;
        UpdatedById = matchSession.UpdatedById;
    }
}