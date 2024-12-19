using Microsoft.AspNetCore.Mvc;
using skat_back.data;
using skat_back.services;

namespace skat_back.controllers;


[ApiController]
[Route("api/[controller]")]
public class MatchdayController: ControllerBase
{
    private readonly MatchdayService _service;

    public MatchdayController(MatchdayService service)
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
    public IActionResult UpdateMatchday(int id, [FromBody] Matchday matchday)
    {
        _service.UpdateMatchday(id, matchday);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMatchday(int id)
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