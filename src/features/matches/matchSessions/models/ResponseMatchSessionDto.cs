using skat_back.features.matchRounds.models;

namespace skat_back.features.matches.matchSessions.models;

/// <summary>
///     Represents the data transfer object (DTO) for a match session response.
/// </summary>
/// <param name="Id">The id of the MatchSession</param>
/// <param name="CreatedById">The User who created the MatchSession</param>
/// <param name="UpdatedById">The User who updated the MatchSession</param>
/// <param name="PlayedAt">The date the MatchSession happened</param>
/// <param name="MatchRounds">A collection of match Rounds that happened on that day</param>
public sealed record ResponseMatchSessionDto(
    int Id,
    string PlayedAt,
    ICollection<ResponseMatchRoundDto> MatchRounds,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    string CreatedById,
    string? UpdatedById = null
);