namespace skat_back.data;

public class Match
{
    public int Id { get; }
    public string Name { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }
}