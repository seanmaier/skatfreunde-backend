using Microsoft.EntityFrameworkCore;
using skat_back.data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<MatchDay> MatchDays { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // ===========Matches===========
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

        
        // ===========BlogPosts===========
        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Slug)
            .IsUnique();

        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.Status);

        modelBuilder.Entity<BlogPost>()
            .HasIndex(b => b.CreatedAt);
        
        
        // ===========Users===========
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
        });
        
        base.OnModelCreating(modelBuilder);
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}