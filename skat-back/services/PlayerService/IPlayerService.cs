using skat_back.DTO.PlayerDTO;

namespace skat_back.services.PlayerService;

public interface IPlayerService
{
    Task<IEnumerable<PlayerResponseDto>> GetAllPlayersAsync();
    Task<PlayerResponseDto?> GetPlayerByIdAsync(string id);
    Task<PlayerResponseDto> CreatePlayerAsync(CreatePlayerDto dto);
    Task<bool> UpdatePlayerAsync(string id, UpdatePlayerDto dto);
    Task<bool> DeletePlayerAsync(string id);
}