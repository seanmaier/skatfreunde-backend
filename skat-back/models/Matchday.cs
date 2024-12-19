namespace skat_back.data;

public class Matchday
{
    public int Id { get; set; }
    public string Date { get; set; }
    public List<Match> Matches { get; set; }

    public Matchday()
    {
        Matches = new List<Match>();
    }
}