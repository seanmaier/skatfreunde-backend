using skat_back.features.matches.playerRoundStatistics.models;

namespace skat_back.features.statistics;

public interface IStatisticsRepository
{
    Task<ICollection<PlayerRoundStats>?> GetAnnualPlayerData(DateTime startOfTheYear);
    Task<int> GetYearMatchDay(DateTime year);
    Task<ICollection<PlayerRoundStats>?> GetMatchSessionAsync(DateTime weekStart, DateTime weekEnd);
}