using skat_back.DTO;
using skat_back.DTO.PlayerDTO;
using skat_back.models;

namespace skat_back.services.PlayerService;

public interface IPlayerService
{
    Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
    Task<PlayerDto?> GetPlayerByIdAsync(string id);
    Task<PlayerDto> CreatePlayerAsync(CreatePlayerDto playerDto);
    Task<bool> UpdatePlayerAsync(string id, UpdatePlayerDto playerDtoDto);
    Task<bool> DeletePlayerAsync(string id);
}