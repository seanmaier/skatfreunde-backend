namespace skat_back.DTO.UserDTO;

public record UpdateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);