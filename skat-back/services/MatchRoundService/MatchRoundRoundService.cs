using skat_back.data;
using skat_back.models;

namespace skat_back.services.MatchRoundService;

public class MatchRoundRoundService(AppDbContext repository) : IMatchRoundService
{
    public IEnumerable<MatchRound> GetAll()
    {
        return repository.MatchRounds.ToList();
    }

    public MatchRound? GetById(string id)
    {
        return repository.MatchRounds.Find(id);
    }

    public void Add(MatchRound matchRound)
    {
        repository.Add(matchRound);
        repository.SaveChanges();
    }

    public void Update(string id, MatchRound updatedMatchRound)
    {
        var existingMatchRound = repository.MatchRounds.Find(id);
        if (existingMatchRound == null)
            throw new Exception("MatchRound not found");

        existingMatchRound.RoundNumber = updatedMatchRound.RoundNumber;
        existingMatchRound.UpdatedAt = DateTime.UtcNow;

        repository.SaveChanges();
    }

    public void Delete(string id)
    {
        var matchRound = repository.MatchRounds.Find(id);
        if (matchRound == null)
            throw new Exception("MatchRound not found");
        repository.Remove(matchRound);
    }
}