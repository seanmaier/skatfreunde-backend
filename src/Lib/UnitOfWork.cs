using Microsoft.EntityFrameworkCore.Storage;
using skat_back.data;
using skat_back.Features.BlogPosts;
using skat_back.features.matches.matchRounds;
using skat_back.features.matches.matchSessions;
using skat_back.features.matches.playerRoundStatistics;
using skat_back.Features.PlayerRoundStatistics;
using skat_back.features.players;

namespace skat_back.Lib;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IBlogPostRepository? _blogPostRepository;
    private IMatchRoundRepository? _matchRoundRepository;
    private IMatchSessionRepository? _matchSessionRepository;
    private IPlayerRoundStatsRepository? _playerRoundStatsRepository;
    private IPlayerRepository? _players;

    public IPlayerRepository Players => _players ??= new PlayerRepository(context);
    public IMatchSessionRepository MatchSessions => _matchSessionRepository ??= new MatchSessionRepository(context);
    public IMatchRoundRepository MatchRounds => _matchRoundRepository ??= new MatchRoundRepository(context);

    public IPlayerRoundStatsRepository PlayerRoundStats =>
        _playerRoundStatsRepository ??= new PlayerRoundStatsRepository(context);

    public IBlogPostRepository BlogPosts => _blogPostRepository ??= new BlogPostRepository(context);

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