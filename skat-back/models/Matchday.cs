using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class Matchday
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }

    public List<Match> Matches { get; set; } = new List<Match>();
}