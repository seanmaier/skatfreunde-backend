using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.utilities.mapping;

namespace skat_back.Features.Players;

/// <summary>
///     Represents the service implementation for managing players.
/// </summary>
/// <param name="uow">To be removed</param>
/// <param name="db">The database context</param>
public class PlayerService(IUnitOfWork uow, AppDbContext db) : IPlayerService
{
    public async Task<ICollection<ResponsePlayerDto>> GetAllAsync()
    {
        return await db.Players.Select(p => p.ToDto()).ToListAsync();
    }

    public async Task<ResponsePlayerDto?> GetByIdAsync(int id)
    {
        var player = await db.Players.FindAsync(id);
        return player?.ToDto();
    }

    public async Task<ResponsePlayerDto> CreateAsync(CreatePlayerDto dto)
    {
        var player = dto.ToEntity();

        db.Players.Add(player);
        await uow.CommitAsync();

        return player.ToDto(); //TODO check if fits to database
    }

    public async Task<bool> UpdateAsync(int id, UpdatePlayerDto dto)
    {
        var existing = await db.Players.FindAsync(id);
        if (existing == null)
            return false;

        existing.Name = dto.Name;
        existing.UpdatedAt = DateTime.UtcNow;

        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var player = await db.Players.FindAsync(id);
        if (player == null)
            return false;
        db.Players.Remove(player);
        await uow.CommitAsync();
        return true;
    }
}