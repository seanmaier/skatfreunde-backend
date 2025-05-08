namespace skat_back.DTO.PlayerDTO;

public record UpdatePlayerDto(
    Guid CreatedById,
    string Name
);