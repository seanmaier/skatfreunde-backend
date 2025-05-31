using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.playerRoundStatistics.models;

namespace skat_back.features.statistics;

public class StatisticsRepository(AppDbContext context) : IStatisticsRepository
{
    public async Task<ICollection<PlayerRoundStats>> GetAnnualPlayerData(int year)
    {
        var startOfTheYear = new DateTime(year, 01, 01);

        var playerStats = await context.PlayerRoundStats
            .Where(prs => prs.MatchRound.MatchSession.CreatedAt >= startOfTheYear)
            .Include(playerRoundStats => playerRoundStats.Player)
            .Include(playerRoundStats => playerRoundStats.MatchRound)
            .ThenInclude(matchRound => matchRound.MatchSession)
            .ToListAsync();

        return playerStats;
    }

    public async Task<int> GetYearMatchDay(int year)
    {
        var startOfTheYear = new DateTime(year, 01, 01);

        return await context.MatchRounds
            .Where(m => m.MatchSession.CreatedAt >= startOfTheYear)
            .CountAsync();
    }

    public async Task<ICollection<PlayerRoundStats>> GetMatchSession(string calendarWeek)
    {
        return await context.PlayerRoundStats
            .Where(prs => prs.MatchRound.MatchSession.CalendarWeek == calendarWeek)
            .Include(playerRoundStats => playerRoundStats.Player)
            .Include(playerRoundStats => playerRoundStats.MatchRound)
            .ThenInclude(matchRound => matchRound.MatchSession)
            .ToListAsync();
    }
}