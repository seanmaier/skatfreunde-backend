using Microsoft.EntityFrameworkCore;
using skat_back.data;

namespace skat_back.services;

public class MatchDayService
{
    private readonly Repository<MatchDay> _repository;

    public MatchDayService(Repository<MatchDay> repository)
    {
        _repository = repository;
    }

    public IEnumerable<MatchDay> GetAllMatchDays()
    {
        return _repository.GetAll();
    }

    public MatchDay? GetMatchById(int id)
    {
        return _repository.GetById(id);
    }

    public void AddMatchDay(MatchDay matchDay)
    {
        _repository.Add(matchDay);
    }

    public void UpdateMatchDay(int id, MatchDay updatedMatchDay)
    {
        _repository.Update(id, updatedMatchDay, (existing, updated) =>
        {
            existing.Matches = updated.Matches;
            existing.Date = updated.Date;
        } );
    }

    public void DeleteMatchDay(int id)
    {
        _repository.Delete(id);
    }
}