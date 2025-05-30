using System.ComponentModel.DataAnnotations;
using skat_back.features.matchRounds.models;
using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.features.playerRoundStatistics.models;

/// <summary>
///     Represents a player round statistics entity for the Database.
/// </summary>
public class PlayerRoundStats: IEntity
{
    public int Id { get; set; }
    [Range(0, int.MaxValue)] public required int Points { get; set; }
    public required int Won { get; set; }
    public required int Lost { get; set; }

    /*--------------------Navigation  Properties--------------------*/

    [Required] public required int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    [Required] public int MatchRoundId { get; set; }
    public MatchRound MatchRound { get; set; } = null!;
    
    /*------------------------Updater Logic------------------------*/
    
    public void UpdateFrom(PlayerRoundStats playerRoundStats)
    {
        Points = playerRoundStats.Points;
        Won = playerRoundStats.Won;
        Lost = playerRoundStats.Lost;
        PlayerId = playerRoundStats.PlayerId;
    }
}