namespace skat_back.Features.Players;

/// <summary>
///     Represents a data transfer object (DTO) for creating a player.
/// </summary>
/// <param name="CreatedByUserId">The user who created the player</param>
/// <param name="Name">The name of the player</param>
public record CreatePlayerDto(
    string CreatedByUserId,
    string Name
);