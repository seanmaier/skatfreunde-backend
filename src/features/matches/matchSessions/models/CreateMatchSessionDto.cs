using skat_back.features.matches.matchRounds.models;

namespace skat_back.features.matches.matchSessions.models;

/// <summary>
///     Represents the data transfer object (DTO) for creating a match session.
/// </summary>
/// <param name="CreatedById">The User who created the Match</param>
/// <param name="PlayedAt">The date the Match happened</param>
/// <param name="MatchRounds">A collection of matches that happened that day</param>
public sealed record CreateMatchSessionDto(
    string CreatedById,
    string PlayedAt,
    ICollection<CreateMatchRoundDto> MatchRounds
);