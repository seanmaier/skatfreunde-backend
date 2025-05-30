namespace skat_back.Lib;

/// <summary>
/// Base interface for all entities in the application. Should be added to indicate that an entity has an id
/// </summary>
public interface IEntity
{
    int Id { get; set; }
}