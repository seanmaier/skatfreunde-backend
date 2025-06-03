using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using skat_back.data;
using skat_back.features.auth.models;
using skat_back.features.email;
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
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Set to Always in production
});

// Add User Secrets
builder.Configuration.AddUserSecrets<Program>();

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

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")));

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));


// dependency injection registrations
builder.Services.ConfigureServices();

if (builder.Environment.IsDevelopment())
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenLocalhost(5000); // HTTP
        options.ListenLocalhost(5001, listenOptions => // HTTPS
        {
            listenOptions.UseHttps();
        });
    });
else
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
        options.HttpsPort = 443;
    });

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

if (!app.Environment.IsDevelopment()) app.UseHsts();

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var db = services.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    await AppDbSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();


// For testing purposes, we can keep this empty class
public partial class Program
{
}