using System.ComponentModel.DataAnnotations;

namespace skat_back.models;

public class MatchSession : BaseEntity
{
    [Key] public int Id { get; set; }

    public string CalendarWeek { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public Guid CreatedByUserId { get; set; }

    public User CreatedByUser { get; set; } = null!;

    public ICollection<MatchRound> MatchRounds { get; set; } = new HashSet<MatchRound>();
}