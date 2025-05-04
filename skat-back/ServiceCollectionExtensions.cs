using skat_back.models;
using skat_back.repositories;
using skat_back.services;
using skat_back.services.BlogPostService;
using skat_back.services.MatchRoundService;
using skat_back.services.MatchSessionService;
using skat_back.services.PlayerRoundResultsService;
using skat_back.services.PlayerService;
using skat_back.services.UserService;

namespace skat_back;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<PlayerService>();
        services.AddScoped<UserService>();
        services.AddScoped<MatchRoundService>();
        services.AddScoped<PlayerRoundResultService>();
        services.AddScoped<BlogPostService>();
        services.AddScoped<MatchSessionService>();

        return services;
    }
}
