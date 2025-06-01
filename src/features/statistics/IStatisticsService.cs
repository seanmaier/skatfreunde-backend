using skat_back.features.statistics.models;

namespace skat_back.features.statistics;

public interface IStatisticsService
{
    Task<AnnualDataResponseDto?> GetAnnualData(AnnualDataQuery year);
    Task<MatchSessionDto?> GetMatchSession(MatchSessionQuery query);
}