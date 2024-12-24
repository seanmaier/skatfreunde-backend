using skat_back.controllers;
using skat_back.data;
using SQLitePCL;

namespace skat_back.services;

public class MatchService: IService<Match>
{
    private readonly Repository<Match> _repository;

    public MatchService(Repository<Match> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Match> GetAll()
    {
        return _repository.GetAll();
    }

    public Match? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Add(Match match)
    {
        _repository.Add(match);
    }

    public void Update(int id, Match updatedMatch)
    {
        _repository.Update(id, updatedMatch, (existing, updated) =>
        {
            existing.Lost = updated.Lost;
            existing.Won = updated.Won;
            existing.Player = updated.Player;
            existing.Table = updated.Table;
            existing.UpdatedAt = updated.UpdatedAt;
        });
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}