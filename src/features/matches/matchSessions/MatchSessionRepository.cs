using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.matches.matchSessions.models;
using skat_back.Lib;

namespace skat_back.features.matches.matchSessions;

public class MatchSessionRepository(AppDbContext context)
    : Repository<MatchSession>(context), IMatchSessionRepository
{
    private readonly DbSet<MatchSession> _matchSessions = context.MatchSessions;

    public override async Task<PagedResult<MatchSession>> GetAllAsync(PaginationParameters parameters)
    {
        var query = _matchSessions.AsQueryable();

        var totalCount = await query.CountAsync();

        var matchSessions = await query
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundStats)
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<MatchSession>(matchSessions, parameters.PageNumber, parameters.PageSize, totalCount);
    }

    public override async Task<MatchSession?> GetByIdAsync(int id)
    {
        await LoadTables(id);
        return await base.GetByIdAsync(id);
    }

    private async Task LoadTables(int id)
    {
        var matchSession = await _matchSessions
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundStats)
            .FirstOrDefaultAsync(ms => ms.Id == id);

        if (matchSession == null)
            throw new InvalidOperationException($"MatchSession with ID {id} not found.");
    }
}