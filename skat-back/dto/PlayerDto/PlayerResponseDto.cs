namespace skat_back.DTO.PlayerDTO;

public record PlayerResponseDto(
    string Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);