namespace skat_back.DTO.UserDTO;

public sealed record ResponseUserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password
);