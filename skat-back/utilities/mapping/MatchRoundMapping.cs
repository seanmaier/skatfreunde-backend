using skat_back.dto.MatchRoundDto;
using skat_back.models;

namespace skat_back.utilities.mapping;

public static class MatchRoundMapping
{
    public static MatchRound ToEntity(this CreateMatchRoundDto entity)
    {
        return new MatchRound
        {
            RoundNumber = entity.RoundNumber,
            PlayerRoundResults = entity.PlayerRoundResults.Select(x => x.ToEntity()).ToList()
        };
    }

    public static MatchRound ToEntity(this UpdateMatchRoundDto entity)
    {
        return new MatchRound
        {
            RoundNumber = entity.RoundNumber,
            PlayerRoundResults = entity.PlayerRoundStats.Select(x => x.ToEntity()).ToList()
        };
    }

    public static ResponseMatchRoundDto ToDto(this MatchRound entity)
    {
        return new ResponseMatchRoundDto(
            entity.Id,
            entity.MatchSessionId,
            entity.RoundNumber,
            entity.PlayerRoundResults.Select(x => x.ToDto()).ToList(),
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}