using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;

namespace skat_back.data;

public class Player
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    public List<Match> Matches { get; set; } = new List<Match>();
}