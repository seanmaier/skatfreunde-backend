using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace skat_back.features.auth.models;

/// <summary>
///     Represents a user entity for the Database.
/// </summary>
public class ApplicationUser : IdentityUser<Guid>
{
    [DataType(DataType.Time)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Time)] public DateTime? UpdatedAt { get; set; }

    public bool IsApproved { get; set; } = false;

    public List<RefreshToken> RefreshTokens { get; set; } = [];
}