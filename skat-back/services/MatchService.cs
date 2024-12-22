using skat_back.data;
using SQLitePCL;

namespace skat_back.services;

public class MatchService
{
    private readonly Repository<Match> _repository;

    public MatchService(Repository<Match> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Match> GetAllMatches()
    {
        return _repository.GetAll();
    }

    public Match? GetMatchById(int id)
    {
        return _repository.GetById(id);
    }

    public void AddMatch(Match match)
    {
        _repository.Add(match);
    }

    public void UpdateMatch(int id, Match updatedMatch)
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

    public void DeleteMatch(int id)
    {
        _repository.Delete(id);
    }
}