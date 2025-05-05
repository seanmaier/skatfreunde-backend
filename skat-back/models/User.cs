using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.utilities.constants.ValidationConstants;

namespace skat_back.models;

public class User : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public required string FirstName { get; set; }

    [Required]
    [MinLength(MinNameLength)]
    [MaxLength(MaxNameLength)]
    public required string LastName { get; set; }

    [Required]
    [MinLength(MinPasswordLength)]
    [MaxLength(MaxPasswordLength)]
    public required string Password { get; set; }

    [Required]
    [MinLength(MinEmailLength)]
    [MaxLength(MaxEmailLength)]
    [EmailAddress]
    public required string Email { get; set; }
}