namespace skat_back.DTO.UserDTO;

public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);