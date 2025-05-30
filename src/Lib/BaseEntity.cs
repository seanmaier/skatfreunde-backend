using skat_back.features.auth.models;

namespace skat_back.Lib;

/// <summary>
/// Base entity for the database models.
/// </summary>
public abstract class BaseEntity : IEntity
{
    public DateTime CreatedAt { get; set; }
    public Guid CreatedById { get; set; }
    public ApplicationUser CreatedBy { get; set; } = null!;
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedById { get; set; }
    public ApplicationUser UpdatedBy { get; set; } = null!;
    public int Id { get; set; }
}