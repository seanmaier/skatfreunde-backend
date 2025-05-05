using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class Player : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    public ICollection<PlayerRoundResult> PlayerRoundResults { get; set; } = new List<PlayerRoundResult>();
}