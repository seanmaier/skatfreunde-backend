using skat_back.features.players.models;

namespace skat_back.utilities.mapping;

public static class PlayerMapping
{
    public static Player ToEntity(this CreatePlayerDto entity)
    {
        return new Player
        {
            Name = entity.Name,
            CreatedById = Guid.Parse(entity.CreatedById),
        };
    }

    public static Player ToEntity(this UpdatePlayerDto entity)
    {
        return new Player
        {
            Name = entity.Name,
            UpdatedById = Guid.Parse(entity.UpdatedById)
        };
    }

    public static ResponsePlayerDto ToResponse(this Player entity)
    {
        return new ResponsePlayerDto(
            entity.Id,
            entity.Name,
            entity.CreatedAt,
            entity.UpdatedAt,
            entity.CreatedById.ToString(),
            entity.UpdatedById.ToString()
        );
    }
}