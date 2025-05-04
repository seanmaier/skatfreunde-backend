namespace skat_back.repositories;

public interface IRepository<T> where T: class
{
    T? GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(int id, T entity, Action<T, T> action);
    void Delete(int id);
}