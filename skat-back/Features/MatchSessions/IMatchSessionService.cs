using skat_back.dto.MatchSessionDto;
using skat_back.models;

namespace skat_back.services.MatchSessionService;

public interface IMatchSessionService : IService<ResponseMatchSessionDto, CreateMatchSessionDto, UpdateMatchSessionDto, int>
{
    // Add specific methods here
}