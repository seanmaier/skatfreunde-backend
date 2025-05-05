using Serilog;
using skat_back.services.BlogPostService;
using skat_back.services.MatchRoundService;
using skat_back.services.MatchSessionService;
using skat_back.services.PlayerRoundResultsService;
using skat_back.services.PlayerService;
using skat_back.services.UserService;

namespace skat_back;

public static class ServiceCollectionExtensions
{
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IMatchRoundService, MatchRoundRoundService>();
        services.AddScoped<IPlayerRoundResultService, PlayerRoundResultService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IMatchSessionService, MatchSessionService>();
        services.AddScoped<IPlayerService, PlayerService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton(Log.Logger);
    }
}