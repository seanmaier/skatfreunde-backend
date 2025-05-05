using Microsoft.AspNetCore.Mvc;
using skat_back.DTO.PlayerDTO;
using skat_back.services.PlayerService;

namespace skat_back.controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController(IPlayerService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllPlayers()
    {
        IEnumerable<PlayerResponseDto> players = await service.GetAllPlayersAsync();
        return Ok(players);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlayerById(int id)
    {
        var player = await service.GetPlayerByIdAsync(id);
        if (player == null)
            return NotFound();

        return Ok(player);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerDto playerDto)
    {
        var newPlayer = await service.CreatePlayerAsync(playerDto);
        return CreatedAtAction(nameof(GetPlayerById), new { id = newPlayer.Id }, newPlayer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlayer(int id, [FromBody] UpdatePlayerDto playerDtoDto)
    {
        var updated = await service.UpdatePlayerAsync(id, playerDtoDto);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var deleted = await service.DeletePlayerAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}