using skat_back.dto.PlayerRoundResultDto;

namespace skat_back.services.PlayerRoundResultsService;

public interface IPlayerRoundResultService : IService<ResponsePlayerRoundStatsDto, CreatePlayerRoundStatsDto,
    UpdatePlayerRoundStatsDto, int>
{
}