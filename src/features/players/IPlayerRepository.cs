using skat_back.features.players.models;

namespace skat_back.Features.Players;

public interface IPlayerRepository
{
    Task<ICollection<Player>> GetAllAsync();
    Task<Player?> GetByIdAsync(int id);

    Task<Player> CreateAsync(Player newPlayer);
    void Delete(Player player);
}