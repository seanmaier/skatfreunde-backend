using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class Player : BaseEntity
{
    [Key] public int Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    public ICollection<PlayerRoundStats> PlayerRoundResults { get; set; } = new HashSet<PlayerRoundStats>();
}