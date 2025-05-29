using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.features.players;

public interface IPlayerRepository: IRepository<Player>
{
    Task<Player?> GetByNameAsync(string name);
}