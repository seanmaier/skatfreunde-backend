namespace skat_back.services;

public interface IService<T> where T: class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(int id, T entity);
    void Delete(int id);
}