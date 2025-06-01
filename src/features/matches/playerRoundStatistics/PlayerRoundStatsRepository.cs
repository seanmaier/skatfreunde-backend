using skat_back.data;
using skat_back.features.matches.playerRoundStatistics.models;
using skat_back.Lib;

namespace skat_back.features.matches.playerRoundStatistics;

public class PlayerRoundStatsRepository(AppDbContext context)
    : Repository<PlayerRoundStats>(context), IPlayerRoundStatsRepository
{
}