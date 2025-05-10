using Microsoft.AspNetCore.Mvc;
using skat_back.services;

namespace skat_back.controllers;

/// <summary>
///     A generic controller providing basic CRUD operations for a specified entity type.
///     This controller is designed to work with a service layer that handles the entity's business logic.
/// </summary>
/// <typeparam name="TResponse">The type of the response DTO.</typeparam>
/// <typeparam name="TCreate">The type of the DTO used for creating entities.</typeparam>
/// <typeparam name="TUpdate">The type of the DTO used for updating entities.</typeparam>
/// <typeparam name="TId">The type of the entity identifier.</typeparam>
/// <typeparam name="TService">The type of the Business Service</typeparam>
[ApiController]
[Route("api/[controller]")]
public class BaseController<TResponse, TCreate, TUpdate, TId, TService>(TService service) : ControllerBase
    where TResponse : class
    where TCreate : class
    where TUpdate : class
    where TId : struct
    where TService : IService<TResponse, TCreate, TUpdate, TId>
{
    private TService _service = service;

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(TId id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null)
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TCreate dto)
    {
        var item = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = (item as dynamic).Id }, item);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TId id, [FromBody] TUpdate dto)
    {
        var result = await _service.UpdateAsync(id, dto);
        if (!result)
            return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TId id)
    {
        var result = await _service.DeleteAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}