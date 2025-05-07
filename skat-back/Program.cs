using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;
using skat_back;
using skat_back.data;
using skat_back.utilities.validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => { options.Filters.Add<ValidationFilter>(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

/*builder.Services.AddFluentValidationAutoValidation();*/
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


// Configure Serilog
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// dependency injection registrations
builder.Services.ConfigureServices();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

app.UseSerilogRequestLogging();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseCors();

app.UseHttpsRedirection();
app.MapControllers();

/*using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DataSeeder.Seed(context);
}*/

app.Run();