using skat_back.data;

namespace skat_back;

public class UnitOfWork(AppDbContext context): IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private bool _disposed;
    
    public async Task<bool> CommitAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Rollback()
    {
    }

    public void Dispose()
    {
        if (_disposed) return;
        
        _context.Dispose();
        _disposed = true;
    }
}