using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.matches.matchSessions.models;
using skat_back.Lib;

namespace skat_back.features.matches.matchSessions;

public class MatchSessionRepository(AppDbContext context) : Repository<MatchSession>(context), IMatchSessionRepository
{
    private readonly AppDbContext _context = context;

    public override async Task<ICollection<MatchSession>> GetAllAsync()
    {
        var matchSessions = await _context.MatchSessions
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundStats).ToListAsync();

        return matchSessions;
    }

    public override async Task<MatchSession?> GetByIdAsync(int id)
    {
        await LoadTables(id);
        return await base.GetByIdAsync(id);
    }

    private async Task LoadTables(int id)
    {
        var matchSession = await _context.MatchSessions
            .Include(ms => ms.MatchRounds)
            .ThenInclude(mr => mr.PlayerRoundStats)
            .FirstOrDefaultAsync(ms => ms.Id == id);

        if (matchSession == null)
            throw new InvalidOperationException($"MatchSession with ID {id} not found.");
    }
}