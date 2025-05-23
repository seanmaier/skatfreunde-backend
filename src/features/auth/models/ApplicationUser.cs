using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using skat_back.Lib;

namespace skat_back.features.auth.models;

/// <summary>
///     Represents a user entity for the Database.
/// </summary>
public class ApplicationUser : IdentityUser
{
    [DataType(DataType.Time)] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Time)] public DateTime? UpdatedAt { get; set; }

    public List<RefreshToken> RefreshTokens { get; set; } = [];
}