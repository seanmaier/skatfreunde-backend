namespace skat_back.DTO.UserDTO;

public record UserResponseDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Password
);