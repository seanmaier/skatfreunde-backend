using Microsoft.EntityFrameworkCore;
using skat_back.data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<Matchday> MatchDays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>()
            .HasOne(m => m.MatchDay)
            .WithMany(m => m.Matches)
            .HasForeignKey(m => m.MatchDayId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Match>()
            .HasIndex(m => m.MatchDayId);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.Player)
            .WithMany(p => p.Matches)
            .HasForeignKey(m => m.PlayerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Match>()
            .HasIndex(m => m.PlayerId);
        
        base.OnModelCreating(modelBuilder);
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}