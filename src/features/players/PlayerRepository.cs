using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.players.models;

namespace skat_back.Features.Players;

public class PlayerRepository(AppDbContext context) : IPlayerRepository
{
    public async Task<ICollection<Player>> GetAllAsync()
    {
        return await context.Players.ToListAsync();
    }

    public async Task<Player?> GetByIdAsync(int id)
    {
        var player = await context.Players.FindAsync(id);
        return player;
    }

    public async Task<Player> CreateAsync(Player newPlayer)
    {
        await context.Players.AddAsync(newPlayer);
        return newPlayer;
    }

    public void Delete(Player player)
    {
        context.Players.Remove(player);
    }
}