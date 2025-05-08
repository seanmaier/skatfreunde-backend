using skat_back.dto.MatchRoundDto;

namespace skat_back.dto.MatchSessionDto;

public sealed record UpdateMatchSessionDto(
    Guid CreatedByUserId,
    string CalendarWeek,
    ICollection<UpdateMatchRoundDto> MatchRounds
);