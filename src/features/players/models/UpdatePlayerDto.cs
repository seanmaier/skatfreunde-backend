namespace skat_back.features.players.models;

/// <summary>
///     Represents a data transfer object (DTO) for updating a player.
/// </summary>
/// <param name="UpdatedById">The user who created the player</param>
/// <param name="Name">The name of the player</param>
public record UpdatePlayerDto(
    string UpdatedById,
    string Name
);