using skat_back.data;
using skat_back.features.matchRounds.models;
using skat_back.features.matchSessions.models;
using skat_back.features.playerRoundStatistics.models;
using skat_back.utilities.mapping;

namespace skat_back.Lib;

public static class DataSeeder
{
    public static async Task Seed(AppDbContext context, string userId)
    {
        await context.Database.EnsureCreatedAsync();

        var matchSession = new CreateMatchSessionDto(
            userId,
            "KW12",
            new List<CreateMatchRoundDto>
            {
                new
                (
                    "1",
                    "1",
                    new List<CreatePlayerRoundStatsDto>
                    {
                        new
                        (
                            1,
                            100,
                            0,
                            0
                        )
                    }
                )
            }
        ).ToEntity();

        await context.MatchSessions.AddAsync(matchSession);
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

        await context.SaveChangesAsync();
    }
}