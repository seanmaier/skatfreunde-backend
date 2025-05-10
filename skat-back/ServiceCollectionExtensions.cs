using skat_back.Features.BlogPosts;
using skat_back.Features.MatchRounds;
using skat_back.Features.MatchSessions;
using skat_back.Features.PlayerRoundStatistics;
using skat_back.Features.Players;
using skat_back.Features.Users;

namespace skat_back;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMatchRoundService, MatchRoundService>();
        services.AddScoped<IPlayerRoundStatsService, PlayerRoundStatsService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IMatchSessionService, MatchSessionService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}