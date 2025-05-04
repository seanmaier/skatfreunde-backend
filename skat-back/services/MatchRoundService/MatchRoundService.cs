using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.MatchRoundService;

public class MatchRoundService(IRepository<MatchRound> repository) : IMatchService
{
    public IEnumerable<MatchRound> GetAll()
    {
        return repository.GetAll();
    }

    public MatchRound? GetById(int id)
    {
        return repository.GetById(id);
    }

    public void Add(MatchRound matchRound)
    {
        repository.Add(matchRound);
    }

    public void Update(int id, MatchRound updatedMatchRound)
    {
        repository.Update(id, updatedMatchRound, (existing, updated) =>
        {
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}