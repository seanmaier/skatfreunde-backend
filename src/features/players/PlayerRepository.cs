using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.features.players;

public class PlayerRepository(AppDbContext context)
    : Repository<Player>(context), IPlayerRepository
{
    private readonly AppDbContext _context = context;

    /// <summary>
    ///     Retrieves a player by their name.
    /// </summary>
    /// <param name="name">The name of the player to retrieve.</param>
    /// <returns>The player with the specified name, or null if not found.</returns>
    public async Task<Player?> GetByNameAsync(string name)
    {
            return await _context.Players.FirstOrDefaultAsync(p => p.Name == name);
    }
}