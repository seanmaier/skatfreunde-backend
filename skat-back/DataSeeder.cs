using skat_back.data;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Players.Any())
        {
            context.Players.AddRange(
                new Player { FirstName = "John", LastName = "Doe", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Player { FirstName = "Jane", LastName = "Smith",CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                new Player { FirstName = "Sam", LastName = "Fisher",CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
            );

            context.SaveChanges();
        }
    }
}