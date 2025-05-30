using skat_back.data;
using skat_back.features.matchRounds.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.Lib;
using skat_back.utilities.mapping;

namespace skat_back.Features.MatchSessions;

/// <summary>
///     Represents the service for managing match sessions.
/// </summary>
public class MatchSessionService(IUnitOfWork unitOfWork)
    : IMatchSessionService
{
    public async Task<ICollection<ResponseMatchSessionDto>> GetAllAsync()
    {
        var matchSessions = await unitOfWork.MatchSessions.GetAllAsync();
        return matchSessions.Select(ms => ms.ToDto()).ToList();
    }

    
    public async Task<ResponseMatchSessionDto?> GetByIdAsync(int id)
    {
        var matchSession = await unitOfWork.MatchSessions.GetByIdAsync(id);
        return matchSession?.ToDto();
    }

    
    public async Task<ResponseMatchSessionDto> CreateAsync(CreateMatchSessionDto dto)
    {
        var session = dto.ToEntity();

        await unitOfWork.MatchSessions.CreateAsync(session);

        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.SaveChangesAsync();
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
        var existingMatchSession = await unitOfWork.MatchSessions.GetByIdAsync(id);
        
        if (existingMatchSession == null)
            return false;

        var matchSession = dto.ToEntity();
        existingMatchSession.UpdateFrom(matchSession);
        
        UpdateRounds(existingMatchSession.MatchRounds, matchSession.MatchRounds);

        await unitOfWork.SaveChangesAsync();
        return true;
    }

    
    public async Task<bool> DeleteAsync(int id)
    {
        var matchSession = await unitOfWork.MatchSessions.GetByIdAsync(id);
        if (matchSession == null)
            return false;
        unitOfWork.MatchSessions.Delete(matchSession);
        await unitOfWork.SaveChangesAsync();
        return true;
    }

    
    private void UpdateRounds(ICollection<MatchRound> existingRounds, ICollection<MatchRound> updatedRounds)
    {
        for (var i = 0; i < existingRounds.Count; i++)
        {
            var existingRound = existingRounds.ElementAt(i);
            var updatedRound = updatedRounds.ElementAtOrDefault(i);
            if (updatedRound == null) continue;

            existingRound.UpdateFrom(updatedRound);
            UpdatePlayerStats(existingRound.PlayerRoundStats, updatedRound.PlayerRoundStats);
        }
    }

    
    private void UpdatePlayerStats(ICollection<PlayerRoundStats> existingResults,
        ICollection<PlayerRoundStats> updatedResults)
    {
        for (var i = 0; i < existingResults.Count; i++)
        {
            var existingStats = existingResults.ElementAt(i);
            var updatedStats = updatedResults.ElementAtOrDefault(i);
            if (updatedStats == null) continue;

            existingStats.UpdateFrom(updatedStats);
        }
    }
}