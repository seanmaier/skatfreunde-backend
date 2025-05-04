using Microsoft.EntityFrameworkCore;
using skat_back.models;

namespace skat_back.data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MatchRound> MatchRounds { get; set; }
    public DbSet<MatchSession> MatchSessions { get; set; }
    public DbSet<PlayerRoundResult> PlayerRoundResults { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchSession>()
            .HasMany(ms => ms.MatchRounds)
            .WithOne(mr => mr.MatchSession)
            .HasForeignKey(mr => mr.MatchSessionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<MatchSession>(entity =>
                entity.HasIndex(ms => ms.DateOfTheWeek).IsUnique());

        modelBuilder.Entity<Player>()
            .HasMany(p => p.PlayerRoundResults)
            .WithOne(prr => prr.Player)
            .HasForeignKey(prr => prr.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Player>(entity =>
            entity.HasIndex(p => p.Name).IsUnique());

        modelBuilder.Entity<PlayerRoundResult>()
            .HasKey(prr => new { prr.MatchRoundId, prr.PlayerId });
        
        // ===========BlogPosts===========
        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();

        // ===========Users===========
        modelBuilder.Entity<User>(entity =>
            entity.HasIndex(u => u.Email).IsUnique());
        
        base.OnModelCreating(modelBuilder);
    }
}