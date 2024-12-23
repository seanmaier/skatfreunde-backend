using Microsoft.AspNetCore.Mvc;

namespace skat_back.controllers;

public interface IService<T> where T: class
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Add(T entity);
    void Update(int id, T entity);
    void Delete(int id);
}