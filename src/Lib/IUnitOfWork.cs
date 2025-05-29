using skat_back.features.players;

namespace skat_back.Lib;

public interface IUnitOfWork : IDisposable
{
    IPlayerRepository Players { get; }
    Task<int> SaveChangesAsync();
}