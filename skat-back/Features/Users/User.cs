using System.ComponentModel.DataAnnotations;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

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