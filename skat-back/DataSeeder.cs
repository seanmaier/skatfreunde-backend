using skat_back.data;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Players.Any())
        {
            context.Players.AddRange(
                new Player { Name = "John Doe", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Player { Name = "Jane Smith", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Player { Name = "Sam Fisher", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            context.SaveChanges();
        }
    }
}