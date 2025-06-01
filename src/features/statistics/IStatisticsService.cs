using skat_back.features.statistics.models;

namespace skat_back.features.statistics;

public interface IStatisticsService
{
    Task<AnnualDataResponseDto> GetAnnualData(int year);
    Task<MatchSessionDto> GetMatchSession(DateTime weekStart);
}