using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace skat_back.data;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string PasswordHash { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Email { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }
}