using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class Match
{
    
    public int Id { get; set; } 
    public int MatchDayId { get; set; } 
    public Matchday MatchDay { get; set; }
    
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    
    public int Points { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }
    public int Table { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}