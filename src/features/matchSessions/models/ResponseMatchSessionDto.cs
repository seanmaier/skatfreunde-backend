using skat_back.Features.MatchRounds;
using skat_back.features.matchRounds.models;

namespace skat_back.features.matchSessions.models;

/// <summary>
///     Represents the data transfer object (DTO) for a match session response.
/// </summary>
/// <param name="Id">The id of the MatchSession</param>
/// <param name="CreatedByUserId">The User who created the MatchSession</param>
/// <param name="CalendarWeek">The CalenderWeek the MatchSession happened</param>
/// <param name="MatchRounds">A collection of match Rounds that happened on that day</param>
public sealed record ResponseMatchSessionDto(
    int Id,
    Guid CreatedByUserId,
    string CalendarWeek,
    ICollection<ResponseMatchRoundDto> MatchRounds,
    DateTime CreatedAt,
    DateTime UpdatedAt
);