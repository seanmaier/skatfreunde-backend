using Microsoft.EntityFrameworkCore;
using skat_back.controllers;
using skat_back.data;

namespace skat_back.services;

public class MatchDayService: IService<MatchDay>
{
    private readonly IRepository<MatchDay> _repository;

    public MatchDayService(IRepository<MatchDay> repository)
    {
        _repository = repository;
    }

    public IEnumerable<MatchDay> GetAll()
    {
        return _repository.GetAll();
    }

    public MatchDay? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Add(MatchDay matchDay)
    {
        _repository.Add(matchDay);
    }

    public void Update(int id, MatchDay updatedMatchDay)
    {
        _repository.Update(id, updatedMatchDay, (existing, updated) =>
        {
            existing.Matches = updated.Matches;
            existing.Date = updated.Date;
        } );
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}