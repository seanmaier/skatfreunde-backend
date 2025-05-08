using skat_back.dto.MatchRoundDto;

namespace skat_back.services.MatchRoundService;

public interface IMatchRoundService : IService<ResponseMatchRoundDto, CreateMatchRoundDto, UpdateMatchRoundDto, int>
{
    // Add specific methods here
}