using skat_back.Features.MatchRounds;
using skat_back.features.matchRounds.models;

namespace skat_back.utilities.mapping;

public static class MatchRoundMapping
{
    public static MatchRound ToEntity(this CreateMatchRoundDto entity)
    {
        return new MatchRound
        {
            RoundNumber = entity.RoundNumber,
            Table = entity.Table,
            PlayerRoundStats = entity.PlayerRoundStats.Select(x => x.ToEntity()).ToList()
        };
    }

    public static MatchRound ToEntity(this UpdateMatchRoundDto entity)
    {
        return new MatchRound
        {
            RoundNumber = entity.RoundNumber,
            Table = entity.Table,
            PlayerRoundStats = entity.PlayerRoundStats.Select(x => x.ToEntity()).ToList()
        };
    }

    public static ResponseMatchRoundDto ToDto(this MatchRound entity)
    {
        return new ResponseMatchRoundDto(
            entity.Id,
            entity.MatchSessionId,
            entity.RoundNumber,
            entity.Table,
            entity.PlayerRoundStats.Select(x => x.ToDto()).ToList(),
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}