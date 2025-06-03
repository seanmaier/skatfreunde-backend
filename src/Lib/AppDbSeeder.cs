using Microsoft.AspNetCore.Identity;
using skat_back.features.auth.models;

namespace skat_back.Lib;

public static class AppDbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var adminEmail = configuration["AdminUser:Email"]
                         ?? Environment.GetEnvironmentVariable("ADMIN_EMAIL");

        if (string.IsNullOrEmpty(adminEmail))
            throw new InvalidOperationException("Admin email is not configured.");

        var adminPassword = configuration["Admin_Password"]
                            ?? Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (string.IsNullOrEmpty(adminPassword))
            throw new InvalidOperationException("Admin password is not configured.");

        var adminUserName = configuration["AdminUser:Username"]
                            ?? Environment.GetEnvironmentVariable("ADMIN_USERNAME");

        if (string.IsNullOrEmpty(adminUserName))
            throw new InvalidOperationException("Admin username is not configured.");

        string[] roles = ["Admin", "Manager", "User"];

        foreach (var role in roles)
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new ApplicationRole { Name = role });

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            var newAdmin = new ApplicationUser { UserName = adminUserName, Email = adminEmail, EmailConfirmed = true };
            var result = await userManager.CreateAsync(newAdmin, adminPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create admin user: " +
                                    string.Join(", ", result.Errors.Select(e => e.Description)));

            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}