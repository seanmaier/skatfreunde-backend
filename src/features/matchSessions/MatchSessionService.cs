using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.matchRounds.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.mapping;

namespace skat_back.Features.MatchSessions;

/// <summary>
///     Represents the service for managing match sessions.
/// </summary>
/// <param name="db">The Database context</param>
public class MatchSessionService(AppDbContext db)
    : IMatchSessionService
{
    public async Task<ICollection<ResponseMatchSessionDto>> GetAllAsync()
    {
        return await db.MatchSessions.Select(ms => ms.ToDto()).ToListAsync();
    }

    public async Task<ResponseMatchSessionDto?> GetByIdAsync(int id)
    {
        var matchSession = await db.MatchSessions.FindAsync(id);
        return matchSession?.ToDto();
    }

    public async Task<ResponseMatchSessionDto> CreateAsync(CreateMatchSessionDto dto)
    {
        var session = dto.ToEntity();

        db.MatchSessions.Add(session);

        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return session.ToDto();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<bool> UpdateAsync(int id, UpdateMatchSessionDto dto)
    {
        var existingMatchSession = await db.MatchSessions // TODO check logic
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundStats)
            .SingleOrDefaultAsync();

        if (existingMatchSession == null)
            return false;

        var matchSession = dto.ToEntity();
        db.Entry(existingMatchSession).CurrentValues.SetValues(matchSession);

        UpdateRounds(existingMatchSession.MatchRounds, matchSession.MatchRounds);

        await db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var matchSession = await db.MatchSessions.FindAsync(id);
        if (matchSession == null)
            return false;
        db.MatchSessions.Remove(matchSession);
        await db.SaveChangesAsync();
        return true;
    }

    private void UpdateRounds(ICollection<MatchRound> existingRounds, ICollection<MatchRound> updatedRounds)
    {
        foreach (var existingRound in existingRounds)
        {
            var updatedRound = updatedRounds.FirstOrDefault(r => r.Id == existingRound.Id);
            if (updatedRound == null) continue;

            db.Entry(existingRound).CurrentValues.SetValues(updatedRound);
            UpdatePlayerStats(existingRound.PlayerRoundStats, updatedRound.PlayerRoundStats);
        }
    }

    private void UpdatePlayerStats(ICollection<PlayerRoundStats> existingResults,
        ICollection<PlayerRoundStats> updatedResults)
    {
        foreach (var existingResult in existingResults)
        {
            var updatedResult = updatedResults.FirstOrDefault(r =>
                r.MatchRoundId == existingResult.MatchRoundId && r.PlayerId == existingResult.PlayerId);
            if (updatedResult == null) continue;

            db.Entry(existingResult).CurrentValues.SetValues(updatedResult);
        }
    }
}