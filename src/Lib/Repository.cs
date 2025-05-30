using Microsoft.EntityFrameworkCore;
using skat_back.data;
using skat_back.Features;

namespace skat_back.Lib;

public class Repository<T>(AppDbContext context) : IRepository<T> where T: class, IEntity
{
    public virtual async Task<ICollection<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        var entity = await context.Set<T>().FindAsync(id);
        return entity;
    }

    public virtual async Task<T> CreateAsync(T newEntity)
    {
        await context.Set<T>().AddAsync(newEntity);
        return newEntity;
    }
    
    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }
}