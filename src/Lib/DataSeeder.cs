using skat_back.data;

namespace skat_back.Lib;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        /*var player = new Player
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Id = 1,
            Name = "TestPlayer",
            CreatedById = user.Id
        };

        context.Players.Add(player);

        var blogPost = new BlogPost
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Id = 1,
            Title = "Test Blog Post",
            Text = "This is a test blog post.",
            Slug = "test-blog-post",
            Summary = "This is a summary of the test blog post.",
            MetaTitle = "Test Blog Post Meta Title",
            MetaDescription = "This is a meta description for the test blog post.",
            CreatedById = user.Id
        };

        context.BlogPosts.Add(blogPost);

        var matchSession = new MatchSession
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Id = 1,
            CreatedByUserId = user.Id,
            CalendarWeek = "12",
        };

        context.MatchSessions.Add(matchSession);

        var matchRound = new MatchRound
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            Id = 1,
            MatchSessionId = matchSession.Id,
            RoundNumber = "1"
        };

        context.MatchRounds.Add(matchRound);

        var playerRoundStats = new PlayerRoundStats
        {
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            PlayerId = player.Id,
            Points = 100,
            Won = 0,
            Lost = 0,
            Table = "1",
            MatchRoundId = matchRound.Id
        };

        context.PlayerRoundStats.Add(playerRoundStats);*/

        context.SaveChanges();
    }
}