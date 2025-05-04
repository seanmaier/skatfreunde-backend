using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using skat_back.DTO;
using skat_back.DTO.PlayerDTO;
using skat_back.models;
using skat_back.services.PlayerService;

namespace skat_back.controllers;

[ApiController]
[Route("api/players")]
public class PlayersController(IPlayerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPlayers()
    {
        IEnumerable<PlayerDto> players = await service.GetAllPlayersAsync();
        return Ok(players);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerById(string id)
    {
        PlayerDto? player = await service.GetPlayerByIdAsync(id);
        if (player == null)
            return NotFound();

        return Ok(player);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto playerDto)
    {
        PlayerDto newPlayer = await service.CreatePlayerAsync(playerDto);
        return CreatedAtAction(nameof(GetPlayerById), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(string id, [FromBody] UpdatePlayerDto playerDtoDto)
    {
        bool updated = await service.UpdatePlayerAsync(id, playerDtoDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(string id)
    {
        bool deleted = await service.DeletePlayerAsync(id);
        return deleted? NoContent() : NotFound();
    }
}