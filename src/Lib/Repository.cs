using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.Features;

namespace skat_back.Lib;

public class Repository<T>(AppDbContext context) : IRepository<T>
    where T : class, IEntity
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<PagedResult<T>> GetAllAsync(PaginationParameters parameters)
    {
        var query = _dbSet.AsQueryable();
        
        var totalCount = await query.CountAsync();
        
        var data = await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<T>(data, 1, 1, totalCount);
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        return entity;
    }

    public virtual async Task<T> CreateAsync(T newEntity)
    {
        await _dbSet.AddAsync(newEntity);
        return newEntity;
    }
    
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}