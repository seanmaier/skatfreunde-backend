using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.matches.playerRoundStatistics.models;

namespace skat_back.features.statistics;

public class StatisticsRepository(AppDbContext context)
    : IStatisticsRepository
{
    public async Task<ICollection<PlayerRoundStats>?> GetAnnualPlayerData(DateTime startOfTheYear)
    {
        var playerStats = await context.PlayerRoundStats
            .Where(prs => prs.MatchRound.MatchSession.PlayedAt >= startOfTheYear)
            .Include(playerRoundStats => playerRoundStats.Player)
            .Include(playerRoundStats => playerRoundStats.MatchRound)
            .ThenInclude(matchRound => matchRound.MatchSession)
            .ToListAsync();

        return playerStats.Count != 0 ? playerStats : null;
    }

    public async Task<int> GetYearMatchDay(DateTime startOfTheYear)
    {
        return await context.MatchRounds
            .Where(m => m.MatchSession.CreatedAt >= startOfTheYear)
            .CountAsync();
    }

    public async Task<ICollection<PlayerRoundStats>?> GetMatchSessionAsync(DateTime weekStart, DateTime weekEnd)
    {
        var playerStats = await context.PlayerRoundStats
            .Where(prs => prs.MatchRound.MatchSession.PlayedAt >= weekStart &&
                          prs.MatchRound.MatchSession.PlayedAt < weekEnd)
            .Include(prs => prs.Player)
            .Include(prs => prs.MatchRound)
            .ThenInclude(mr => mr.MatchSession)
            .ToListAsync();

        return playerStats.Count != 0 ? playerStats : null;
    }
}