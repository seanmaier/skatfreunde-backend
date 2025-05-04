using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.MatchSessionService;

public class MatchSessionService(IRepository<MatchSession> repository) : IMatchSessionService
{
    public IEnumerable<MatchSession> GetAll()
    {
        return repository.GetAll();
    }

    public MatchSession? GetById(int id)
    {
        return repository.GetById(id);
    }

    public void Add(MatchSession matchSession)
    {
        repository.Add(matchSession);
    }

    public void Update(int id, MatchSession updatedMatchSession)
    {
        repository.Update(id, updatedMatchSession, (existing, updated) =>
        {
            existing.DateOfTheWeek = updated.DateOfTheWeek;
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}