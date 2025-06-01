using skat_back.features.matches.playerRoundStatistics.models;

namespace skat_back.features.statistics;

public interface IStatisticsRepository
{
    Task<ICollection<PlayerRoundStats>> GetAnnualPlayerData(int year);
    Task<int> GetYearMatchDay(int year);
    Task<ICollection<PlayerRoundStats>> GetMatchSession(DateTime weekStart);
}