using skat_back.Features.Players;

namespace skat_back.utilities.mapping;

public static class PlayerMapping
{
    public static Player ToEntity(this CreatePlayerDto entity)
    {
        return new Player
        {
            Name = entity.Name,
            CreatedById = Guid.Parse(entity.CreatedByUserId)
        };
    }

    public static Player ToEntity(this UpdatePlayerDto entity)
    {
        return new Player
        {
            Name = entity.Name,
            CreatedById = Guid.Parse(entity.CreatedByUserId)
        };
    }

    public static ResponsePlayerDto ToDto(this Player entity)
    {
        return new ResponsePlayerDto(
            entity.Id,
            entity.Name,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}