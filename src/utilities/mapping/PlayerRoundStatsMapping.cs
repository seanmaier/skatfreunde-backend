using skat_back.features.playerRoundStatistics.models;

namespace skat_back.utilities.mapping;

public static class PlayerRoundStatsMapping
{
    public static PlayerRoundStats ToEntity(this CreatePlayerRoundStatsDto entity)
    {
        return new PlayerRoundStats
        {
            PlayerId = entity.PlayerId,
            Points = entity.Points,
            Won = entity.Won,
            Lost = entity.Lost
        };
    }

    public static PlayerRoundStats ToEntity(this UpdatePlayerRoundStatsDto entity)
    {
        return new PlayerRoundStats
        {
            PlayerId = entity.PlayerId,
            Points = entity.Points,
            Won = entity.Won,
            Lost = entity.Lost
        };
    }

    public static ResponsePlayerRoundStatsDto ToDto(this PlayerRoundStats entity)
    {
        return new ResponsePlayerRoundStatsDto
        (
            entity.PlayerId,
            entity.MatchRoundId,
            entity.Points,
            entity.Won,
            entity.Lost,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}