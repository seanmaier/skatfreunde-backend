using skat_back.data;
using skat_back.services;

namespace skat_back.controllers;

public class PlayerController: BaseController<Player, PlayerService>
{
    public PlayerController(PlayerService service) : base(service)
    {
        
    }
}