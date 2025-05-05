using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;


namespace skat_back.models;

public class MatchSession
{
    [Key] public int Id { get; set; }

    public DateTime DateOfTheWeek { get; set; }

    [Required] [MaxLength(MaxIdLength)] public Guid CreatedByUserId { get; set; }

    [Required] public User CreatedByUser { get; set; } = null!;

    public ICollection<MatchRound> MatchRounds { get; set; } = new List<MatchRound>();
}