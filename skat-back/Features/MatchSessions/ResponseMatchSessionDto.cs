using skat_back.dto.MatchRoundDto;

namespace skat_back.dto.MatchSessionDto;

public sealed record ResponseMatchSessionDto(
    int Id,
    Guid CreatedByUserId,
    string CalendarWeek,
    ICollection<ResponseMatchRoundDto> MatchRounds,
    DateTime CreatedAt,
    DateTime UpdatedAt
);