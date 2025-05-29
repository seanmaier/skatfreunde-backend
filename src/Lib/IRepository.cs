namespace skat_back.Lib;

public interface IRepository<T> where T: class
{
    Task<ICollection<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);

    Task<T> CreateAsync(T newEntity);
    void Delete(T entity);
}