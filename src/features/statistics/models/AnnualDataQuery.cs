namespace skat_back.features.statistics.models;

public record AnnualDataQuery(
    DateTime RequestYear
)
{
    public AnnualDataQuery() : this(DateTime.Now)
    {
    }
}