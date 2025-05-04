using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static skat_back.constants.ValidationConstats;

namespace skat_back.models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required] [MaxLength(NameMaxLength)] public required string FirstName { get; set; }

    [Required] [MaxLength(NameMaxLength)] public required string LastName { get; set; }

    [Required]
    [MaxLength(PasswordMaxLength)]
    public required string Password { get; set; }

    [Required]
    [MaxLength(EmailMaxLength)]
    [EmailAddress]
    public required string Email { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}