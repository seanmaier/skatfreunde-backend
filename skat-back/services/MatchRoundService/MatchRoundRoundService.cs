using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.dto.MatchRoundDto;
using skat_back.models;

namespace skat_back.services.MatchRoundService;

public class MatchRoundRoundService(AppDbContext db, IMapper mapper, IUnitOfWork uow) : IMatchRoundService
{
    public async Task<ICollection<ResponseMatchRoundDto>> GetAllAsync()
    {
        return await db.MatchRounds.ProjectTo<ResponseMatchRoundDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ResponseMatchRoundDto?> GetByIdAsync(int id)
    {
        var matchRound = await db.MatchRounds.FindAsync(id);
        return matchRound == null ? null : mapper.Map<ResponseMatchRoundDto>(matchRound);
    }

    public async Task<ResponseMatchRoundDto> CreateAsync(CreateMatchRoundDto dto)
    {
        var matchRound = mapper.Map<MatchRound>(dto);

        db.MatchRounds.Add(matchRound);
        await uow.CommitAsync();

        return mapper.Map<ResponseMatchRoundDto>(matchRound);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMatchRoundDto dto)
    {
        var existingMatchRound = await db.MatchRounds.FindAsync(id);
        if (existingMatchRound == null)
            return false;

        existingMatchRound.RoundNumber = dto.RoundNumber;
        existingMatchRound.UpdatedAt = DateTime.UtcNow;

        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var matchRound = await db.MatchRounds.FindAsync(id);
        if (matchRound == null)
            return false;
        db.Remove(matchRound);
        
        await uow.CommitAsync();
        return true;
    }
}