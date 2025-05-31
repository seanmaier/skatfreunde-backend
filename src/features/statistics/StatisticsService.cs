using skat_back.data;
using skat_back.features.statistics.models;
using skat_back.Lib;

namespace skat_back.features.statistics;

public class StatisticsService(AppDbContext context, ILogger<StatisticsService> logger, IUnitOfWork unitOfWork)
    : IStatisticsService
{
    public async Task<AnnualDataResponseDto> GetAnnualData(int year)
    {
        var playerStats = await unitOfWork.Statistics.GetAnnualPlayerData(year);

        var annualDataQuery = playerStats
            .GroupBy(prs => new { prs.PlayerId, prs.Player.Name })
            .Select(g => new AnnualPlayerData(
                g.Key.Name,
                g.Key.PlayerId,
                g.Select(prs => prs.MatchRound.MatchSession.CalendarWeek).Distinct().Count(),
                g.Sum(prs => prs.Points),
                g.Sum(prs => prs.Points) / g.Select(prs => prs.MatchRound.MatchSession.CalendarWeek).Distinct().Count(),
                g.Sum(prs => prs.Points) -
                g.Count() * 1000, // Assuming 1000 is the average points per game // TODO check logic
                g.Sum(prs => prs.Won),
                g.Sum(prs => prs.Lost)
            ))
            .OrderByDescending(apd => apd.AveragePoints)
            .ToList();

        var matchDay = await unitOfWork.Statistics.GetYearMatchDay(year);
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