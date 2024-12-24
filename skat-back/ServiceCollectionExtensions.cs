using skat_back.services;
using skat_back.services.BlogPostService;
using skat_back.services.MatchDayService;
using skat_back.services.MatchService;
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
        services.AddScoped<MatchService>();
        services.AddScoped<BlogPostService>();
        services.AddScoped<MatchDayService>();

        return services;
    }
}
