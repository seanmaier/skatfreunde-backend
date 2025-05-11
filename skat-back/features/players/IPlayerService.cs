using skat_back.services;

namespace skat_back.Features.Players;

/// <summary>
///     Represents the service interface for managing players.
///     Specific implementations for the Player Service should be provided here.
/// </summary>
public interface IPlayerService : IService<ResponsePlayerDto, CreatePlayerDto, UpdatePlayerDto, int>
{
}