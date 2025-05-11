using skat_back.Features.MatchRounds;

namespace skat_back.Features.MatchSessions;

/// <summary>
///     Represents the data transfer object (DTO) for creating a match session.
/// </summary>
/// <param name="CreatedByUserId">The User who created the Match</param>
/// <param name="CalendarWeek">The Week the Match happened</param>
/// <param name="MatchRounds">A collection of matches that happened that day</param>
public sealed record CreateMatchSessionDto(
    Guid CreatedByUserId,
    string CalendarWeek,
    ICollection<CreateMatchRoundDto> MatchRounds
);