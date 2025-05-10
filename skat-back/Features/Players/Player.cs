using System.ComponentModel.DataAnnotations;
using skat_back.Features.Users;
using skat_back.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.Features.Players;

/// <summary>
///     Represents a player entity for the Database.
/// </summary>
public class Player : BaseEntity
{
    [Key] public int Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required Guid CreatedById { get; set; }
    public User CreatedBy { get; set; } = null!;

    public ICollection<PlayerRoundStatistics.PlayerRoundStats> PlayerRoundResults { get; set; } =
        new HashSet<PlayerRoundStatistics.PlayerRoundStats>();
}