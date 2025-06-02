namespace skat_back.features.statistics.models;

public record MatchSessionQuery(
    DateTime WeekStart
)
{
    public MatchSessionQuery() : this(DateTime.UtcNow)
    {
    }
}