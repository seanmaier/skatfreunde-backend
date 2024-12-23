using Microsoft.AspNetCore.Mvc;
using skat_back.services;

namespace skat_back.controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController<T, TService> : ControllerBase 
    where T : class 
    where TService : IService<T>
{
    protected readonly TService _service;

    protected BaseController(TService service)
    {
        _service = service;
    }

    [HttpGet]
    public virtual IActionResult GetAll()
    {
        var items = _service.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public virtual IActionResult GetById(int id)
    {
        var item = _service.GetById(id);
        return Ok(item);
    }

    [HttpPost]
    public virtual IActionResult Create([FromBody] T item)
    {
        _service.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = (item as dynamic).Id }, item);
    }

    [HttpPut("{id}")]
    public virtual IActionResult Update(int id, [FromBody] T item)
    {
        _service.Update(id, item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}