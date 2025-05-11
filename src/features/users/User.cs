using System.ComponentModel.DataAnnotations;
using skat_back.models;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.Features.Users;

/// <summary>
///     Represents a user entity for the Database.
/// </summary>
public class User : BaseEntity
{
    [Key] public Guid Id { get; set; }

    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public required string FirstName { get; set; }

    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public required string LastName { get; set; }

    [MinLength(MinPasswordLength)]
    [MaxLength(MaxPasswordLength)]
    public required string Password { get; set; }

    [MinLength(MinEmailLength)]
    [MaxLength(MaxEmailLength)]
    [EmailAddress]
    public required string Email { get; set; }
}