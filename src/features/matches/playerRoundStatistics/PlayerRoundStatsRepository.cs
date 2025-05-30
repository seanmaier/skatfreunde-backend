using skat_back.data;
using skat_back.features.matches.playerRoundStatistics;
using skat_back.features.playerRoundStatistics.models;
using skat_back.Lib;

namespace skat_back.Features.PlayerRoundStatistics;

public class PlayerRoundStatsRepository(AppDbContext context)
    : Repository<PlayerRoundStats>(context), IPlayerRoundStatsRepository
{
}