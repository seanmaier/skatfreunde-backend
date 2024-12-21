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
        var matchdays = _service.GetAllMatchDays();
        return Ok(matchdays);
    }

    [HttpGet("{id}")]
    public IActionResult GetMatchById(int id)
    {
        var matchday = _service.GetMatchById(id);
        return Ok(matchday);
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

    [HttpGet("player/{id}")]
    public IActionResult GetPlayerById(int id)
    {
        var player = _service.GetPlayerById(id);
        return Ok(player);
    }
}