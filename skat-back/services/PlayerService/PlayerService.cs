using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.DTO.PlayerDTO;
using skat_back.models;

namespace skat_back.services.PlayerService;

public class PlayerService(IUnitOfWork uow, AppDbContext db, IMapper mapper) : IPlayerService
{
    public async Task<ICollection<ResponsePlayerDto>> GetAllAsync()
    {
        return await db.Players.ProjectTo<ResponsePlayerDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ResponsePlayerDto?> GetByIdAsync(int id)
    {
        var player = await db.Players.FindAsync(id);
        return player == null ? null : mapper.Map<ResponsePlayerDto>(player);
    }

    public async Task<ResponsePlayerDto> CreateAsync(CreatePlayerDto dto)
    {
        var player = mapper.Map<Player>(dto);

        db.Players.Add(player);
        await uow.CommitAsync();

        return mapper.Map<ResponsePlayerDto>(player);
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