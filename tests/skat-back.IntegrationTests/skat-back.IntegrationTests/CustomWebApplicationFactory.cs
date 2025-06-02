using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using skat_back.data;
using skat_back.features.auth.models;
using static skat_back.IntegrationTests.TestConstants;

namespace skat_back.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly SqliteConnection _sqliteConnection;

    public CustomWebApplicationFactory()
    {
        // Set the environment variable to use the test database
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Testing");

        // Add an in-memory database for testing
        _sqliteConnection = new SqliteConnection("DataSource=:memory:");
        _sqliteConnection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            Environment.SetEnvironmentVariable("ADMIN_EMAIL", "admin@example.com");
            Environment.SetEnvironmentVariable("ADMIN_PASSWORD", "Admin123!");
            Environment.SetEnvironmentVariable("ADMIN_USERNAME", "admin");

            // Remove the real AppDbContext
            var descriptor = services
                .SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options => { options.UseSqlite(_sqliteConnection); });

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            SeedTestUser(userManager).GetAwaiter().GetResult();
        });
    }

    private static async Task SeedTestUser(UserManager<ApplicationUser> userManager)
    {
            var testUser = new ApplicationUser
            {
                UserName = TestUserName,
                Email = TestUserMail
            };
            await userManager.CreateAsync(testUser, TestUserPassword);
            await userManager.AddToRoleAsync(testUser, "User");
    }
}