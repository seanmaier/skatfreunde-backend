using skat_back.DTO.PlayerDTO;

namespace skat_back.services.PlayerService;

public interface IPlayerService : IService<ResponsePlayerDto, CreatePlayerDto, UpdatePlayerDto, int>
{
}