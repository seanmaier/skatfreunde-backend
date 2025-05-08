using skat_back.dto.MatchSessionDto;
using skat_back.models;

namespace skat_back.utilities.mapping;

public static class MatchSessionMapping
{
    public static MatchSession ToEntity(this CreateMatchSessionDto dto)
    {
        return new MatchSession
        {
            CreatedAt = default,
            UpdatedAt = default,
            CalendarWeek = dto.CalendarWeek,
            CreatedByUserId = dto.CreatedByUserId,
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }

    public static MatchSession ToEntity(this UpdateMatchSessionDto dto)
    {
        return new MatchSession
        {
            CalendarWeek = dto.CalendarWeek,
            CreatedByUserId = dto.CreatedByUserId,
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }

    public static ResponseMatchSessionDto ToDto(this MatchSession entity)
    {
        return new ResponseMatchSessionDto
        (
            entity.Id,
            entity.CreatedByUserId,
            entity.CalendarWeek,
            entity.MatchRounds.Select(x => x.ToDto()).ToList(),
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}