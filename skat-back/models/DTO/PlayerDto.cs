namespace skat_back.models.DTO;

public class PlayerDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}