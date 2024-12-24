using skat_back.controllers;
using skat_back.data;

namespace skat_back.services.MatchService;

public class MatchService: IMatchService
{
    private readonly IRepository<Match> _repository;

    public MatchService(IRepository<Match> repository)
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