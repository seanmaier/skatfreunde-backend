using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class MatchDay
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public required User User { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    public int TotalPoints { get; set; }
    public int TotalMatchShare { get; set; }
    public int PointsChangeFromLastMatch { get; set; }

    public List<Match> Matches { get; set; } = new List<Match>();
}