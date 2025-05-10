namespace skat_back.DTO.PlayerDTO;

public record ResponsePlayerDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);