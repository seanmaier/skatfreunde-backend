using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using skat_back.features.auth.models;
using skat_back.features.blogPosts.models;
using skat_back.features.matches.matchRounds.models;
using skat_back.features.matches.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<MatchRound> MatchRounds { get; set; }
    public DbSet<MatchSession> MatchSessions { get; set; }
    public DbSet<PlayerRoundStats> PlayerRoundStats { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    break;
            }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MatchSession>(entity =>
            entity.HasIndex(ms => ms.CalendarWeek).IsUnique());

        modelBuilder.Entity<MatchSession>()
            .HasMany(ms => ms.MatchRounds)
            .WithOne(mr => mr.MatchSession)
            .HasForeignKey(mr => mr.MatchSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MatchRound>()
            .HasMany(mr => mr.PlayerRoundStats)
            .WithOne(prs => prs.MatchRound)
            .HasForeignKey(prs => prs.MatchRoundId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Player>(entity =>
            entity.HasIndex(p => p.Name).IsUnique());

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