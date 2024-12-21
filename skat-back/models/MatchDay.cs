using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class MatchDay
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Required]
    public DateTime Date { get; set; }

    public List<Match> Matches { get; set; } = new List<Match>();
}