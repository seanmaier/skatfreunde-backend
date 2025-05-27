using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.statistics.models;

namespace skat_back.features.statistics;

public class StatisticsService(AppDbContext context, ILogger<StatisticsService> logger) : IStatisticsService
{
    public async Task<AnnualDataResponseDto> GetAnnualData(int year)
    {
        var players = await context.Players.ToListAsync();

        var startOfTheYear = new DateTime(year, 01, 01);
        var playerDataQuery = await context.PlayerRoundStats
            .Where(ps => ps.MatchRound.MatchSession.CreatedAt >= startOfTheYear )
            .GroupBy(ps => ps.PlayerId)
            .Select(g => new AnnualPlayerData(
                players.FirstOrDefault(p => p.Id == g.Key)!.Name ?? "Unknown",
                g.Count(),
                g.Sum(ps => ps.Points),
                (int)g.Average(ps => ps.Points),
                g.Sum(ps => ps.Points) - g.Count() * 1000 // Assuming 1000 is the average points per game
            )).ToListAsync();

        var matchDay = await context.MatchSessions
            .Where(matchSessions => matchSessions.CreatedAt >= startOfTheYear)
            .CountAsync();
        logger.LogInformation("Fetched {Count} player data records for annual statistics.", playerDataQuery.Count);
        
        var annualDataResponse = new AnnualDataResponseDto
        (
            PlayersData: playerDataQuery.ToArray(),
            GuestsData: [], // Assuming no guest data is available
            MatchDay: matchDay.ToString(),
            LastUpdated: DateTime.Now
        );
        
        logger.LogInformation("Annual data response created for year {Year}.", year);
        
        return annualDataResponse;
    }
}