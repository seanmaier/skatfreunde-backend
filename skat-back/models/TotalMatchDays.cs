using System.ComponentModel.DataAnnotations;

namespace skat_back.data;

public class TotalMatchDays
{
    [Key]
    public int Id { get; set; }
    
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    // Every entry represents total value 
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int MatchShare { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int Matches { get; set; } = 0;
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int Points { get; set; } = 0;
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int PointsAvg { get; set; } = 0;
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int Won { get; set; } = 0;
    [Range(0, int.MaxValue, ErrorMessage = "Value cannot be negative")]
    public int Lost { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}