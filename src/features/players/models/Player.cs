using System.ComponentModel.DataAnnotations;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.Lib;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.features.players.models;

/// <summary>
///     Represents a player entity for the Database.
/// </summary>
public class Player : BaseEntity
{
    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public string Name { get; set; } = string.Empty;

    /*--------------------Navigation  Properties--------------------*/

    public ICollection<PlayerRoundStats> PlayerRoundStats { get; set; } = new HashSet<PlayerRoundStats>();
    
    /*------------------------Updater Logic------------------------*/
    
    public void UpdateFrom(Player player)
    {
        Name = player.Name;
        UpdatedById = player.UpdatedById;
    }
}