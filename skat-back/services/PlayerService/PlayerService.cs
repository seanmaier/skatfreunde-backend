using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.DTO;
using skat_back.DTO.PlayerDTO;
using skat_back.models;

namespace skat_back.services.PlayerService;

public class PlayerService(IUnitOfWork uow, AppDbContext db) : IPlayerService
{
    public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
    {
        return await db.Players.Select(p => new PlayerDto
        {
            Id = p.Id,
            Name = p.Name,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        }).ToListAsync();
    }

    public async Task<PlayerDto?> GetPlayerByIdAsync(string id)
    {
        return await db.Players
            .Where(p => p.Id == id)
            .Select(player => new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                CreatedAt = player.CreatedAt,
                UpdatedAt = player.UpdatedAt
            })
            .FirstAsync();
    }

    public async Task<PlayerDto> CreatePlayerAsync(CreatePlayerDto entity)
    {
        var player = new Player { Name = entity.Name };
        
        db.Add(player);
        await uow.CommitAsync();

        return new PlayerDto
        {
            Id = player.Id,
            Name = player.Name,
            CreatedAt = player.CreatedAt,
            UpdatedAt = player.UpdatedAt
        };
    }

    public async Task<bool> UpdatePlayerAsync(string id, UpdatePlayerDto entity)
    {
        var existing = db.Players.FirstOrDefault(p => p.Id == id);
        if (existing == null)
            return false;
        
        existing.Name = entity.Name;
        existing.UpdatedAt = DateTime.UtcNow;
     
        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeletePlayerAsync(string id)
    {
        Player? player = await db.Players.FindAsync(id);
        if (player == null)
            return false;
        db.Players.Remove(player);
        await uow.CommitAsync();
        return true;
    }
}