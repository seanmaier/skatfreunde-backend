using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.Features.Players;

/// <summary>
///     Represents the API controller for managing players.
/// </summary>
/// <param name="service">The injected Player Service</param>
public class PlayersController(IPlayerService service)
    : BaseController<ResponsePlayerDto, CreatePlayerDto, UpdatePlayerDto, int, IPlayerService>(service)
{
}