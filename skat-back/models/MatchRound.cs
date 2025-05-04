using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skat_back.models;

public class MatchRound
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string MatchSessionId { get; set; } = null!;
    [Required]
    public required MatchSession MatchSession { get; set; } = null!;
    
    public int RoundNumber { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}