using Microsoft.EntityFrameworkCore.Storage;
using skat_back.Features.BlogPosts;
using skat_back.features.matches.matchRounds;
using skat_back.features.matches.matchSessions;
using skat_back.features.matches.playerRoundStatistics;
using skat_back.features.players;
using skat_back.features.statistics;

namespace skat_back.Lib;

public interface IUnitOfWork : IDisposable
{
    IPlayerRepository Players { get; }
    IMatchSessionRepository MatchSessions { get; }
    IMatchRoundRepository MatchRounds { get; }
    IPlayerRoundStatsRepository PlayerRoundStats { get; }
    IBlogPostRepository BlogPosts { get; }
    IStatisticsRepository Statistics { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
}