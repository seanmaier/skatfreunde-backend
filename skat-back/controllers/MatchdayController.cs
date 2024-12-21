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
        var matchdays = _service.GetAllMatchdays();
        return Ok(matchdays);
    }

    [HttpGet("{id}")]
    public IActionResult GetMatchById(int id)
    {
        var matchday = _service.GetMatchById(id);
        return Ok(matchday);
    }

    [HttpPost]
    public IActionResult CreateMatchday([FromBody] Matchday matchday)
    {
        _service.AddMatchday(matchday);
        return CreatedAtAction(nameof(GetMatchById), new { id = matchday.Id }, matchday);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMatchday(int id, [FromBody] Matchday matchDay)
    {
        _service.UpdateMatchday(id, matchDay);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMatchDay(int id)
    {
        _service.DeleteMatchday(id);
        return NoContent();
    }

    [HttpGet("player/{id}")]
    public IActionResult GetPlayerById(int id)
    {
        var player = _service.GetPlayerById(id);
        return Ok(player);
    }
}