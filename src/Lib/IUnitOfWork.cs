using skat_back.Features.Players;

namespace skat_back.Lib;

public interface IUnitOfWork : IDisposable
{
    IPlayerRepository Players { get; }
    Task<int> SaveChangesAsync();
}