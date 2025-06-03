using skat_back.features.matches.matchSessions.models;
using skat_back.Lib;

namespace skat_back.utilities.mapping;

public static class MatchSessionMapping
{
    public static MatchSession ToEntity(this CreateMatchSessionDto dto)
    {
        return new MatchSession
        {
            PlayedAt = DateTime.Parse(dto.PlayedAt).ToUniversalTime(),
            CreatedById = Guid.Parse(dto.CreatedById),
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }

    public static MatchSession ToEntity(this UpdateMatchSessionDto dto)
    {
        return new MatchSession
        {
            PlayedAt = DateTime.Parse(dto.PlayedAt).ToUniversalTime(),
            UpdatedById = Guid.Parse(dto.UpdatedById),
            MatchRounds = dto.MatchRounds.Select(x => x.ToEntity()).ToList()
        };
    }
    
    public static PagedResult<ResponseMatchSessionDto> ToPagedResult(this PagedResult<MatchSession> pagedResult)
    {
        return new PagedResult<ResponseMatchSessionDto>(
            pagedResult.Data.Select(x => x.ToResponse()).ToList(),
            pagedResult.PageNumber,
            pagedResult.PageSize,
            pagedResult.TotalCount
        );
    }

    public static ResponseMatchSessionDto ToResponse(this MatchSession entity)
    {
        return new ResponseMatchSessionDto
        (
            entity.Id,
            entity.PlayedAt.ToString("O"),
            entity.MatchRounds.Select(x => x.ToResponse()).ToList(),
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.CreatedById.ToString(),
            entity.UpdatedById.ToString()
        );
    }
}