using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.features.statistics.models;
using skat_back.Lib;

namespace skat_back.features.statistics;

public class StatisticsService(AppDbContext context, ILogger<StatisticsService> logger, IUnitOfWork unitOfWork)
    : IStatisticsService
{
    public async Task<AnnualDataResponseDto> GetAnnualData(int year)
    {
        var players = await context.Players.ToListAsync();

        var startOfTheYear = new DateTime(year, 01, 01);

        var playerStats = await context.PlayerRoundStats
            .Where(prs => prs.MatchRound.MatchSession.CreatedAt >= startOfTheYear)
            .ToListAsync(); // Fetch data from the database

        var annualDataQuery = playerStats
            .GroupBy(prs => new { prs.PlayerId, prs.Player.Name })
            .Select(g => new AnnualPlayerData(
                g.Key.Name,
                g.Key.PlayerId,
                g.Count(),
                g.Sum(prs => prs.Points),
                (int)g.Average(prs => prs.Points),
                g.Sum(prs => prs.Points) -
                g.Count() * 1000, // Assuming 1000 is the average points per game // TODO check logic
                g.Sum(prs => prs.Won), // Count of won games
                g.Sum(prs => prs.Lost) // Count of lost games
            ))
            .OrderByDescending(apd => apd.AveragePoints)
            .ToList(); // Perform grouping and projection in memory

        var matchDay = await context.MatchSessions
            .Where(matchSessions => matchSessions.CreatedAt >= startOfTheYear)
            .CountAsync();
        logger.LogInformation("Fetched {Count} player data records for annual statistics.", annualDataQuery.Count);

        var annualDataResponse = new AnnualDataResponseDto
        (
            annualDataQuery.ToArray(),
            [], // Assuming no guest data is available
            matchDay.ToString(),
            DateTime.Now
        );

        logger.LogInformation("Annual data response created for year {Year}.", year);

        return annualDataResponse;
    }
}