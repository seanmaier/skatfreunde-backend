using skat_back.DTO.PlayerDTO;
using skat_back.services.PlayerService;

namespace skat_back.controllers;

public class PlayersController(IPlayerService service)
    : GenericController<PlayerResponseDto, CreatePlayerDto, UpdatePlayerDto, int, IPlayerService>(service)
{
}