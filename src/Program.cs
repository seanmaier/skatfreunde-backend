using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using skat_back.data;
using skat_back.features.auth.models;
using skat_back.features.email;
using skat_back.features.players.models;
using skat_back.Lib;
using skat_back.utilities.middleware;
using skat_back.utilities.validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwagger();

builder.Services.AddProblemDetails();

builder.Services.AddCustomCors();

// Configure CSRF protection
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

// Configure FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

// Configure JWT;
builder.Services.AddCustomJwtAuthentication(builder.Configuration);

builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection("Identity"));

// Configure Rate Limiting
builder.Services.AddCustomLimiter();

// Configure Serilog
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Add User Secrets
builder.Configuration.AddUserSecrets<Program>();

// dependency injection registrations
builder.Services.ConfigureServices();


var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    string[] roles = ["Admin", "Manager", "User"];

    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new ApplicationRole { Name = role });

    const string adminEmail = "sean.maier@gmail.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser is null)
    {
        var newAdmin = new ApplicationUser { UserName = "admin", Email = adminEmail };
        var result = await userManager.CreateAsync(newAdmin, "Admin123!");
        if (result.Succeeded) await userManager.AddToRoleAsync(newAdmin, "Admin");
    }

    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Players.Any())
    {
        var players = new List<Player>
        {
            new()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = 1,
                Name = "Player1",
                CreatedById = adminUser.Id
            },
            new()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Id = 2,
                Name = "Player2",
                CreatedById = adminUser.Id
            }
        };

        context.Players.AddRange(players);
        await context.SaveChangesAsync();
    }

    await DataSeeder.Seed(context, adminUser.Id.ToString());
}

/*using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataSeeder.Seed(context);
}*/

app.Run();