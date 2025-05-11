using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.utilities.mapping;

namespace skat_back.Features.PlayerRoundStatistics;

/// <summary>
///     Represents the service for managing player round statistics.
/// </summary>
/// <param name="db">The database context</param>
public class PlayerRoundStatsService(AppDbContext db) : IPlayerRoundStatsService
{
    public async Task<ICollection<ResponsePlayerRoundStatsDto>> GetAllAsync()
    {
        return await db.PlayerRoundResults.Select(prr => prr.ToDto())
            .ToListAsync();
    }

    public async Task<ResponsePlayerRoundStatsDto?> GetByIdAsync(int id)
    {
        var playerRoundStats = await db.PlayerRoundResults.FindAsync(id);
        return playerRoundStats?.ToDto();
    }

    public async Task<ResponsePlayerRoundStatsDto> CreateAsync(CreatePlayerRoundStatsDto dto)
    {
        var playerRoundStats = dto.ToEntity();
        db.PlayerRoundResults.Add(playerRoundStats);

        await db.SaveChangesAsync();

        return playerRoundStats.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, UpdatePlayerRoundStatsDto dto)
    {
        var existing = await db.PlayerRoundResults.FindAsync(id);
        if (existing == null)
            return false;

        existing.PlayerId = dto.PlayerId;
        existing.Won = dto.Won;
        existing.Lost = dto.Lost;
        existing.Points = dto.Points;
        existing.Table = dto.Table;
        existing.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var playerRoundResult = await db.PlayerRoundResults.FindAsync(id);
        if (playerRoundResult == null)
            return false;

        db.PlayerRoundResults.Remove(playerRoundResult);
        await db.SaveChangesAsync();
        return true;
    }
}