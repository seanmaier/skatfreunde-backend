using Microsoft.AspNetCore.Mvc;

namespace skat_back.controllers;

public interface IService<in T> where T: class
{
    IActionResult GetAll();
    IActionResult GetById(int id);
    IActionResult Add(T entity);
    IActionResult Update(int id, T entity);
    IActionResult Delete(int id);
}