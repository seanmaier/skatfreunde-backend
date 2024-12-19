using System.Runtime.InteropServices.JavaScript;

namespace skat_back.data;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public List<Match> Matches { get; set; } = new List<Match>();
}