namespace skat_back.Features.Users;

/// <summary>
///     Represents a data transfer object (DTO) for creating a user.
/// </summary>
/// <param name="FirstName">The first name of the user</param>
/// <param name="LastName">The last name of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="Password">The password of the user</param>
public sealed record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);