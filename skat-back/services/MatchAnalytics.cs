using skat_back.data;

namespace skat_back.services;

public class MatchAnalytics
{
    private readonly Repository<data.MatchAnalytics> _repository;

    public MatchAnalytics(Repository<data.MatchAnalytics> repository)
    {
        _repository = repository;
    }

    public IEnumerable<data.MatchAnalytics> GetAll()
    {
        return _repository.GetAll();
    }
}