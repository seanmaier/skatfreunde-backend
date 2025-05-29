using skat_back.data;
using skat_back.features.players;

namespace skat_back.Lib;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IPlayerRepository? _players;

    public IPlayerRepository Players => _players ??= new PlayerRepository(context);

    public void Dispose()
    {
        context.Dispose();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}