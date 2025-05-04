using skat_back.data;

namespace skat_back.repositories;

public class Repository<T>(AppDbContext context) : IRepository<T>
    where T : class
{
    public T? GetById(int id)
    {
        return context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return context.Set<T>().ToList();
    }

    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Update(int id, T updatedEntity, Action<T, T> updateAction)
    {
        var entity = context.Set<T>().Find(id);

        if (entity == null) return;

        updateAction(entity, updatedEntity);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = context.Set<T>().Find(id);
        
        if (entity == null) return;

        context.Set<T>().Remove(entity);
        context.SaveChanges();
    }
}