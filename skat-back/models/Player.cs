using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class Player
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PlayerRoundResult> PlayerRoundResults { get; set; } = new List<PlayerRoundResult>();
}