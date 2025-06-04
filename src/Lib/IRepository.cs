namespace skat_back.Lib;

public interface IRepository<T> where T : class
{
    Task<PagedResult<T>> GetAllAsync(PaginationParameters parameters);
    Task<T?> GetByIdAsync(int id);

    Task<T> CreateAsync(T newEntity);
    Task Delete(T entity);
}