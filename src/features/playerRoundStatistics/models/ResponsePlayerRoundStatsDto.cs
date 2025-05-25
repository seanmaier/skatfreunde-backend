namespace skat_back.features.playerRoundStatistics.models;

/// <summary>
///     Represents a data transfer object (DTO) for player round statistics response.
/// </summary>
/// <param name="PlayerId">The player, that the statistics are meant for. Composite primary key</param>
/// <param name="MatchRoundId">The match, that the statistics took place. Composite primary key</param>
/// <param name="Points">The points, the player achieved during the round</param>
/// <param name="Won">The won games of the player in a round</param>
/// <param name="Lost">The lost games of the player in a round</param>
public sealed record ResponsePlayerRoundStatsDto(
    int PlayerId,
    int MatchRoundId,
    int Points,
    int Won,
    int Lost,
    DateTime CreatedAt,
    DateTime UpdatedAt
);