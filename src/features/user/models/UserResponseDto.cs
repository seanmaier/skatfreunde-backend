namespace skat_back.features.user.models;

public record UserResponseDto(
    string Id,
    string Username,
    string Email,
    List<string> Roles);