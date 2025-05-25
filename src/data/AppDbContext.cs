using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using skat_back.features.auth.models;
using skat_back.features.blogPosts.models;
using skat_back.features.matchRounds.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.features.players.models;

namespace skat_back.data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MatchRound> MatchRounds { get; set; }
    public DbSet<MatchSession> MatchSessions { get; set; }
    public DbSet<PlayerRoundStats> PlayerRoundStats { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchSession>()
            .HasMany(ms => ms.MatchRounds)
            .WithOne(mr => mr.MatchSession)
            .HasForeignKey(mr => mr.MatchSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MatchSession>(entity =>
            entity.HasIndex(ms => ms.CalendarWeek).IsUnique());

        modelBuilder.Entity<Player>()
            .HasMany(p => p.PlayerRoundResults)
            .WithOne(prr => prr.Player)
            .HasForeignKey(prr => prr.PlayerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>(entity =>
            entity.HasIndex(p => p.Name).IsUnique());

        modelBuilder.Entity<PlayerRoundStats>()
            .HasKey(prr => new { prr.MatchRoundId, prr.PlayerId });

        // ===========BlogPosts===========
        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();

        // ===========Users===========
        modelBuilder.Entity<ApplicationUser>(entity =>
            entity.HasIndex(u => u.Email).IsUnique());

        modelBuilder.Entity<ApplicationUser>(entity =>
            entity.HasIndex(u => u.UserName).IsUnique());

        modelBuilder.Entity<ApplicationUser>(entity =>
            entity.HasMany(e => e.RefreshTokens)
                .WithOne(rft => rft.ApplicationUser)
                .HasForeignKey(rft => rft.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade));

        base.OnModelCreating(modelBuilder);
    }
}