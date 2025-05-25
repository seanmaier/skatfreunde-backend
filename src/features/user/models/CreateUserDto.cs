namespace skat_back.features.user.models;

public record CreateUserDto(
    string Username,
    string Password,
    string Email,
    string Role
);