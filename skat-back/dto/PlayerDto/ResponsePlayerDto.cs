namespace skat_back.DTO.PlayerDTO;

public record ResponsePlayerDto(
    string Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);