namespace skat_back.features.user.models;

public record UpdateUserDto(
    string UserId,
    string? Username,
    string? Email
);