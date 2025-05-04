using Microsoft.AspNetCore.Mvc;
using skat_back.models;
using skat_back.models.DTO;
using skat_back.services.PlayerService;

namespace skat_back.controllers;

public class PlayerController(PlayerService service) : BaseController<Player, PlayerService>(service)
{
    [HttpGet("{id}")]
    public override IActionResult GetById(int id)
    {
        var player = service.GetById(id);
        if (player == null)
        {
            return NotFound();
        }

        var playerDto = new PlayerDto
        {
            Id = player.Id,
            Name = player.Name,
            CreatedAt = player.CreatedAt,
        };
        
        return Ok(playerDto);
    }
}