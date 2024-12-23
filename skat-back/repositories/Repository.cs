namespace skat_back.services;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    
    protected Repository(AppDbContext context)
    {
        _context = context;
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        _context.SaveChanges();
    }

    public void Update(int id, T updatedEntity, Action<T, T> updateAction)
    {
        var entity = _context.Set<T>().Find(id);

        if (entity == null) return;

        updateAction(entity, updatedEntity);
        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        var entity = _context.Set<T>().Find(id);
        
        if (entity == null) return;

        _context.Set<T>().Remove(entity);
        _context.SaveChanges();
    }
}