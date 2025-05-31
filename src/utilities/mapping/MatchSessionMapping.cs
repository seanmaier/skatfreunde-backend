using skat_back.features.matches.matchSessions.models;
using skat_back.features.matchSessions.models;

namespace skat_back.utilities.mapping;

public static class MatchSessionMapping
{
    public static MatchSession ToEntity(this CreateMatchSessionDto dto)
    {
        return new MatchSession
        {
            CalendarWeek = dto.CalendarWeek,
            CreatedById = Guid.Parse(dto.CreatedById),
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }

    public static MatchSession ToEntity(this UpdateMatchSessionDto dto)
    {
        return new MatchSession
        {
            CalendarWeek = dto.CalendarWeek,
            UpdatedById = Guid.Parse(dto.UpdatedById),
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }

    public static ResponseMatchSessionDto ToDto(this MatchSession entity)
    {
        return new ResponseMatchSessionDto
        (
            entity.Id,
            entity.CalendarWeek,
            entity.MatchRounds.Select(x => x.ToResponse()).ToList(),
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.CreatedById.ToString(),
            entity.UpdatedById.ToString()
        );
    }
}