using Microsoft.EntityFrameworkCore.Storage;
using skat_back.data;
using skat_back.Features.MatchRounds;
using skat_back.Features.MatchSessions;
using skat_back.Features.PlayerRoundStatistics;
using skat_back.features.players;

namespace skat_back.Lib;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IPlayerRepository? _players;
    private IMatchSessionRepository? _matchSessionRepository;
    private IMatchRoundRepository? _matchRoundRepository;
    private IPlayerRoundStatsRepository? _playerRoundStatsRepository;

    public IPlayerRepository Players => _players ??= new PlayerRepository(context);
    public IMatchSessionRepository MatchSessions => _matchSessionRepository ??= new MatchSessionRepository(context);
    public IMatchRoundRepository MatchRounds => _matchRoundRepository ??= new MatchRoundRepository(context);
    public IPlayerRoundStatsRepository PlayerRoundStats => _playerRoundStatsRepository ??= new PlayerRoundStatsRepository(context);
    
    public void Dispose()
    {
        context.Dispose();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}