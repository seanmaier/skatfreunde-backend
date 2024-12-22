using Microsoft.AspNetCore.Mvc;
using skat_back.data;
using skat_back.services;

namespace skat_back.controllers;


[ApiController]
[Route("api/[controller]")]
public class MatchdayController: ControllerBase
{
    private readonly MatchDayService _service;

    public MatchdayController(MatchDayService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAllMatchDays()
    {
        var matchDays = _service.GetAllMatchDays();
        return Ok(matchDays);
    }

    [HttpGet("{id}")]
    public IActionResult GetMatchById(int id)
    {
        var matchDay = _service.GetMatchById(id);
        return Ok(matchDay);
    }

    [HttpPost]
    public IActionResult CreateMatchday([FromBody] MatchDay matchDay)
    {
        _service.AddMatchDay(matchDay);
        return CreatedAtAction(nameof(GetMatchById), new { id = matchDay.Id }, matchDay);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMatchday(int id, [FromBody] MatchDay matchDay)
    {
        _service.UpdateMatchDay(id, matchDay);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMatchDay(int id)
    {
        _service.DeleteMatchDay(id);
        return NoContent();
    }
}