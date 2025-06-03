using skat_back.features.statistics.models;
using skat_back.Lib;

namespace skat_back.features.statistics;

public class StatisticsService(ILogger<StatisticsService> logger, IUnitOfWork unitOfWork)
    : IStatisticsService
{
    public async Task<MatchSessionDto?> GetMatchSession(MatchSessionQuery query)
    {
        var weekStart = query.WeekStart.Date;
        var weekEnd = weekStart.Date.AddDays(7);

        var playerStats = await unitOfWork.Statistics.GetMatchSessionAsync(weekStart, weekEnd);

        if (playerStats == null)
        {
            logger.LogWarning("No player statistics found for the week starting {WeekStart}.", weekStart);
            return null;
        }

        var matchSession = new MatchSessionDto(
            playerStats.First().MatchRound.MatchSession.PlayedAt.ToString("O"),
            playerStats.First().MatchRound.MatchSession.CreatedAt,
            playerStats
                .GroupBy(prs => new { prs.PlayerId, prs.Player.Name })
                .Select(g =>
                {
                    var matchShare =
                        (float)g.Sum(rs => rs.Won + rs.Lost) /
                        playerStats.Sum(rs => rs.Won + rs.Lost);
                    var series = g.Select(prs => new SeriesDto(prs.Points, prs.Won, prs.Lost)).ToList();

                    return new PlayerMatchDayDataDto(
                        g.Key.Name,
                        matchShare,
                        g.Sum(rs => rs.Points),
                        series
                    );
                })
                .ToList()
        );

        return matchSession;
    }


    public async Task<AnnualDataResponseDto?> GetAnnualData(AnnualDataQuery query)
    {
        var startOfTheYear = new DateTime(query.RequestYear.Year, 1, 1);

        var playerStats = await unitOfWork.Statistics.GetAnnualPlayerData(startOfTheYear);

        if (playerStats is null)
        {
            logger.LogWarning("No player statistics found for the year {Year}.", query.RequestYear.Year);
            return null;
        }

        var latestCalendarWeek = playerStats.Select(prs => prs.MatchRound.MatchSession.PlayedAt).Max();

        var annualDataQuery = playerStats
            .GroupBy(prs => new { prs.PlayerId, prs.Player.Name })
            .Select(g =>
            {
                var distinctMatchDays = g.Select(prs => prs.MatchRound.MatchSession.PlayedAt).Distinct().Count();
                var totalPoints = g.Sum(prs => prs.Points);
                var lastMatchPoints = g.Where(prs =>
                        prs.MatchRound.MatchSession.PlayedAt == g.Max(stats => stats.MatchRound.MatchSession.PlayedAt))
                    .Sum(prs => prs.Points);
                var averagePoints = totalPoints / distinctMatchDays;
                var difference = averagePoints - (totalPoints - lastMatchPoints) / (distinctMatchDays - 1);

                return new AnnualPlayerData(
                    g.Key.Name,
                    g.Key.PlayerId,
                    distinctMatchDays,
                    totalPoints,
                    averagePoints,
                    difference,
                    g.Sum(prs => prs.Won),
                    g.Sum(prs => prs.Lost)
                );
            })
            .OrderByDescending(apd => apd.AveragePoints)
            .ToList();

        var matchDay = await unitOfWork.Statistics.GetYearMatchDay(startOfTheYear);
        logger.LogInformation("Fetched {Count} player data records for annual statistics.", annualDataQuery.Count);

        var annualDataResponse = new AnnualDataResponseDto
        (
            annualDataQuery.ToArray(),
            [], // Assuming no guest data is available // TODO add guest data
            matchDay.ToString(),
            latestCalendarWeek.ToString("O")
        );

        logger.LogInformation("Annual data response created for year {Year}.", startOfTheYear);

        return annualDataResponse;
    }
}