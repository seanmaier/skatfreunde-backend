using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.services.PlayerRoundResultsService;

public interface IPlayerRoundStatsService : IService<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto,
    UpdatePlayerRoundStatsDto, int>
{
}