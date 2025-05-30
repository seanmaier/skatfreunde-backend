using Microsoft.EntityFrameworkCore.Storage;
using skat_back.Features.MatchRounds;
using skat_back.Features.MatchSessions;
using skat_back.Features.PlayerRoundStatistics;
using skat_back.features.players;

namespace skat_back.Lib;

public interface IUnitOfWork : IDisposable
{
    IPlayerRepository Players { get; }
    IMatchSessionRepository MatchSessions { get; }
    IMatchRoundRepository MatchRounds { get; }
    IPlayerRoundStatsRepository PlayerRoundStats { get; }
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
}