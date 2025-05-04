namespace skat_back.DTO.UserDTO;

public record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);