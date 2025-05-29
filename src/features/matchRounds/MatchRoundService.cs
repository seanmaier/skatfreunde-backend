using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.matchRounds.models;
using skat_back.utilities.mapping;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the service for managing match rounds.
/// </summary>
/// <param name="db">The Database context</param>
public class MatchRoundService(AppDbContext db) : IMatchRoundService
{
    public async Task<ICollection<ResponseMatchRoundDto>> GetAllAsync()
    {
        return await db.MatchRounds.Select(mr => mr.ToDto()).ToListAsync();
    }

    public async Task<ResponseMatchRoundDto?> GetByIdAsync(int id)
    {
        var matchRound = await db.MatchRounds.FindAsync(id);
        return matchRound?.ToDto();
    }

    public async Task<ResponseMatchRoundDto> CreateAsync(CreateMatchRoundDto dto)
    {
        var matchRound = dto.ToEntity();

        db.MatchRounds.Add(matchRound);
        await db.SaveChangesAsync();
        
        return matchRound.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, UpdateMatchRoundDto dto)
    {
        var existingMatchRound = await db.MatchRounds.FindAsync(id);
        if (existingMatchRound == null)
            return false;

        existingMatchRound.RoundNumber = dto.RoundNumber;

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var matchRound = await db.MatchRounds.FindAsync(id);
        if (matchRound == null)
            return false;
        db.Remove(matchRound);

        await db.SaveChangesAsync();
        return true;
    }
}