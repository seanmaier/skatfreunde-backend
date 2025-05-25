namespace skat_back.features.user.models;

public record UpdateUserDto(
    string Id,
    string Username,
    string Email,
    string Role
);