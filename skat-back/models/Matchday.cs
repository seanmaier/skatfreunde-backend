namespace skat_back.data;

public class Matchday
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public List<Match> Matches { get; set; } = new List<Match>();
}