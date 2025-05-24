using System.ComponentModel.DataAnnotations;

namespace skat_back.features.auth.models;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(512)] public required string Token { get; set; }

    public required DateTime Expires { get; set; }

    public required DateTime Created { get; set; }

    [MaxLength(64)] public required string CreatedByIp { get; set; } = string.Empty;

    public DateTime? Revoked { get; set; }

    [MaxLength(64)] public string? RevokedByIp { get; set; }

    [MaxLength(512)] public string? UserAgent { get; set; }

    [MaxLength(512)] public string? ReplacedByToken { get; set; }

    // IsExpired is private because IsActive uses property to check if
    // the token is expired. IsActive should only be used outside
    private bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => Revoked == null && !IsExpired;


    /*--------------------Navigation  Properties--------------------*/

    [Required] [MaxLength(128)] public required string ApplicationUserId { get; set; }

    public ApplicationUser ApplicationUser { get; set; } = null!;
}