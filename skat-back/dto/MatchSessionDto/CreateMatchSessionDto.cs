using skat_back.dto.MatchRoundDto;

namespace skat_back.dto.MatchSessionDto;

public sealed record CreateMatchSessionDto(
    Guid CreatedByUserId,
    string CalendarWeek,
    ICollection<CreateMatchRoundDto> MatchRounds
);