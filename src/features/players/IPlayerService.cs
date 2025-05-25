using skat_back.features.players.models;
using skat_back.Lib;

namespace skat_back.Features.Players;

/// <summary>
///     Represents the service interface for managing players.
///     Specific implementations for the Player Service should be provided here.
/// </summary>
public interface IPlayerService : IService<ResponsePlayerDto, CreatePlayerDto, UpdatePlayerDto, int>
{
}