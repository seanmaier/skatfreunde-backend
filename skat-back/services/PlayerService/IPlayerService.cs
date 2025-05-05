using skat_back.DTO.PlayerDTO;

namespace skat_back.services.PlayerService;

public interface IPlayerService
{
    Task<IEnumerable<PlayerResponseDto>> GetAllPlayersAsync();
    Task<PlayerResponseDto?> GetPlayerByIdAsync(int id);
    Task<PlayerResponseDto> CreatePlayerAsync(CreatePlayerDto dto);
    Task<bool> UpdatePlayerAsync(int id, UpdatePlayerDto dto);
    Task<bool> DeletePlayerAsync(int id);
}