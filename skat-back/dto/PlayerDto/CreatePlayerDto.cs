namespace skat_back.DTO.PlayerDTO;

public record CreatePlayerDto(
    Guid CreatedById,
    string Name
);