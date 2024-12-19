using Microsoft.EntityFrameworkCore;
using skat_back.data;

public class AppDbContext : DbContext
{
    public DbSet<Match> Games { get; set; }
    public DbSet<Matchday> Matchdays { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}