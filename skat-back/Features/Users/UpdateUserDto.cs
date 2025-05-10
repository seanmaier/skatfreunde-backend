namespace skat_back.DTO.UserDTO;

public sealed record UpdateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);