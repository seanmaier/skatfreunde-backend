using skat_back.data;

namespace skat_back.services;

public class TotalMatchDayService
{
    private readonly Repository<TotalMatchDays> _repository;

    public TotalMatchDayService(Repository<TotalMatchDays> repository)
    {
        _repository = repository;
    }

    public IEnumerable<TotalMatchDays> GetAllTotalMatchDays()
    {
        return _repository.GetAll();
    }
}