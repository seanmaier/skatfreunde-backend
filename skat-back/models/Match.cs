using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class Match
{
    [Key]
    public int Id { get; set; } 
    
    public int MatchDayId { get; set; }
    public Matchday MatchDay { get; set; }
    
    public int PlayerId { get; set; }
    public Player Player { get; set; }
    
    [Required]
    public int Points { get; set; }
    [Required]
    public int Won { get; set; }
    [Required]
    public int Lost { get; set; }
    [Required]
    public int Table { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}