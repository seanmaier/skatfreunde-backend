using System.ComponentModel.DataAnnotations;

namespace skat_back.features.auth.models;

/// <summary>
///     Represents a data transfer object (DTO) for creating a user.
/// </summary>
/// <param name="Username">The first name of the user</param>
/// <param name="Email">The email of the user</param>
/// <param name="Password">The password of the user</param>
public sealed record RegisterDto(
    string Username,
    [Required] [EmailAddress] string Email,
    [Required]
    [StringLength(100, MinimumLength = 8)]
    string Password
);