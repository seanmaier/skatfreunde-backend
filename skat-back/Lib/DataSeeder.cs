using skat_back.data;
using skat_back.models;

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Players.Any()) return;
        context.SaveChanges();
    }
}