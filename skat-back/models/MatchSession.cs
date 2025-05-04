using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace skat_back.models;

public class MatchSession
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;
    
    public DateTime DateOfTheWeek { get; set; }
    [Required]
    [MaxLength(100)]
    public string CreatedByUserId { get; set; } = null!;
    [Required]
    public User CreatedByUser { get; set; } = null!;
    
    public ICollection<MatchRound> MatchRounds { get; set; } = new List<MatchRound>();
}