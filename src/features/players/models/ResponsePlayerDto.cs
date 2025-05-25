namespace skat_back.features.players.models;

/// <summary>
///     Represents a data transfer object (DTO) for a player response.
/// </summary>
/// <param name="Id">The id of the Player</param>
/// <param name="Name">The name of the Player</param>
public record ResponsePlayerDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);