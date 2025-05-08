using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.dto.MatchSessionDto;
using skat_back.models;

namespace skat_back.services.MatchSessionService;

public class MatchSessionService(IUnitOfWork uow, AppDbContext db, IMapper mapper)
    : IMatchSessionService
{
    public async Task<ICollection<ResponseMatchSessionDto>> GetAllAsync()
    {
        return await db.MatchSessions.ProjectTo<ResponseMatchSessionDto>(mapper.ConfigurationProvider).ToListAsync();
    }

    public async Task<ResponseMatchSessionDto?> GetByIdAsync(int id)
    {
        var matchSession = await db.MatchSessions.FindAsync(id);
        return matchSession == null ? null : mapper.Map<ResponseMatchSessionDto>(matchSession);
    }

    public async Task<ResponseMatchSessionDto> CreateAsync(CreateMatchSessionDto dto)
    {
        /*var matchSession = mapper.Map<MatchSession>(dto);

        db.MatchSessions.Add(matchSession);*/

        var session = new MatchSession
        {
            CreatedByUserId = dto.CreatedByUserId,
            CalendarWeek = dto.CalendarWeek,
            MatchRounds = dto.MatchRound.Select(roundDto => new MatchRound
            {
                RoundNumber = roundDto.RoundNumber,
                PlayerRoundResult = roundDto.PlayerRoundResults.Select(playerRoundDto => new PlayerRoundStats
                {
                    PlayerId = playerRoundDto.PlayerId,
                    Points = playerRoundDto.Points,
                    Won = playerRoundDto.Won,
                    Lost = playerRoundDto.Lost,
                    Table = playerRoundDto.Table
                }).ToList()
            }).ToList()
        };

        db.MatchSessions.Add(session);

        await using var transaction = await db.Database.BeginTransactionAsync();

        try
        {
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return mapper.Map<ResponseMatchSessionDto>(session);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        
        await uow.CommitAsync();

        //return mapper.Map<ResponseMatchSessionDto>(matchSession);
    }

    public async Task<bool> UpdateAsync(int id, UpdateMatchSessionDto dto)
    {
        var existingMatchSession = await db.MatchSessions
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundResult)
            .SingleOrDefaultAsync();

        if (existingMatchSession == null)
            return false;

        var matchSession = mapper.Map(dto, existingMatchSession);

        UpdateRounds(existingMatchSession.MatchRounds, matchSession.MatchRounds);

        await uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var matchSession = await db.MatchSessions.FindAsync(id);
        if (matchSession == null)
            return false;
        db.MatchSessions.Remove(matchSession);
        await uow.CommitAsync();
        return true;
    }

    private void UpdateRounds(ICollection<MatchRound> existingRounds, ICollection<MatchRound> updatedRounds)
    {
        foreach (var existingRound in existingRounds)
        {
            var updatedRound = updatedRounds.FirstOrDefault(r => r.Id == existingRound.Id);
            if (updatedRound == null) continue;

            db.Entry(existingRound).CurrentValues.SetValues(updatedRound);
            UpdatePlayerResults(existingRound.PlayerRoundResult, updatedRound.PlayerRoundResult);
        }
    }

    private void UpdatePlayerResults(ICollection<PlayerRoundStats> existingResults,
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