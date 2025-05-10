namespace skat_back.Features.PlayerRoundStatistics;

/// <summary>
///     Represents a data transfer object (DTO) for updating player round statistics.
/// </summary>
/// <param name="PlayerId">The player, that the statistics are meant for. Composite primary key</param>
/// <param name="MatchRoundId">The match, that the statistics took place. Composite primary key</param>
/// <param name="Points">The points, the player achieved during the round</param>
/// <param name="Won">The won games of the player in a round</param>
/// <param name="Lost">The lost games of the player in a round</param>
/// <param name="Table">The table the game was played at</param>
public sealed record UpdatePlayerRoundStatsDto(
    int MatchRoundId,
    int PlayerId,
    int Points,
    int Won,
    int Lost,
    string Table
);