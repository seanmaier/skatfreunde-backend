using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.dto.PlayerRoundResultDto;
using skat_back.models;

namespace skat_back.services.PlayerRoundResultsService;

public class PlayerRoundResultService(AppDbContext db, IMapper mapper, IUnitOfWork uow) : IPlayerRoundResultService
{
    public async Task<ICollection<ResponsePlayerRoundStatsDto>> GetAllAsync()
    {
        return await db.PlayerRoundResults.ProjectTo<ResponsePlayerRoundStatsDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ResponsePlayerRoundStatsDto?> GetByIdAsync(int id)
    {
        var playerRoundStats = await db.PlayerRoundResults.FindAsync(id);
        return playerRoundStats == null ? null : mapper.Map<ResponsePlayerRoundStatsDto>(playerRoundStats);
    }

    public async Task<ResponsePlayerRoundStatsDto> CreateAsync(CreatePlayerRoundStatsDto dto)
    {
        var playerRoundStats = mapper.Map<PlayerRoundStats>(dto);
        db.PlayerRoundResults.Add(playerRoundStats);

        await uow.CommitAsync();

        return mapper.Map<ResponsePlayerRoundStatsDto>(playerRoundStats);
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

        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var playerRoundResult = await db.PlayerRoundResults.FindAsync(id);
        if (playerRoundResult == null)
            return false;

        db.PlayerRoundResults.Remove(playerRoundResult);
        await uow.CommitAsync();
        return true;
    }
}