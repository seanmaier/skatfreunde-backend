using skat_back.features.matchRounds.models;

namespace skat_back.features.matches.matchSessions.models;

/// <summary>
///     Represents the data transfer object (DTO) for updating a match session.
/// </summary>
/// <param name="UpdatedById">The User who created the Match</param>
/// <param name="PlayedAt">The date the Match happened</param>
/// <param name="MatchRounds">A collection of matches that happened that day</param>
public sealed record UpdateMatchSessionDto(
    string UpdatedById,
    string PlayedAt,
    ICollection<UpdateMatchRoundDto> MatchRounds
);