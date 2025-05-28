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
        
        var adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        var adminUserName = Environment.GetEnvironmentVariable("ADMIN_USERNAME") ?? "admin";
        
        if (string.IsNullOrEmpty(adminEmail) || string.IsNullOrEmpty(adminPassword))
            throw new InvalidOperationException("Admin credentials are not set in environment variables.");
        
        string[] roles = ["Admin", "Manager", "User"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new ApplicationRole { Name = role });
        }
        
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            var newAdmin = new ApplicationUser { UserName = adminUserName, Email = adminEmail, EmailConfirmed = true};
            var result = await userManager.CreateAsync(newAdmin, adminPassword);
            if (!result.Succeeded)
                throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));

            await userManager.AddToRoleAsync(newAdmin, "Admin");
        }
    }
}