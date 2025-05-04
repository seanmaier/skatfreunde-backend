namespace skat_back.services;

public interface IService<T> where T: class
{
    IEnumerable<T> GetAll();
    T? GetById(string id);
    void Add(T entity);
    void Update(string id, T entity);
    void Delete(string id);
}