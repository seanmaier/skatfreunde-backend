using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.PlayerRoundResultsService;

public class PlayerRoundResultService(IRepository<PlayerRoundResult> repository) : IPlayerRoundResultService
{
    public IEnumerable<PlayerRoundResult> GetAll()
    {
        return repository.GetAll();
    }

    public PlayerRoundResult? GetById(int id)
    {
        return repository.GetById(id);
    }

    public void Add(PlayerRoundResult entity)
    {
        repository.Add(entity);
    }

    public void Update(int id, PlayerRoundResult entity)
    {
        repository.Update(id, entity, (existing, updated) =>
        {
            existing.Points = updated.Points;
            existing.Won = updated.Won;
            existing.Lost = updated.Lost;
            existing.Table = existing.Table;
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}