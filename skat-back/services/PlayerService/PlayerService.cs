using skat_back.models;
using skat_back.repositories;

namespace skat_back.services.PlayerService;

public class PlayerService(IRepository<Player> repository) : IPlayerService
{
    public IEnumerable<Player> GetAll()
    {
        return repository.GetAll();
    }

    public Player? GetById(int id)
    {
        return repository.GetById(id);
    }

    public void Add(Player entity)
    {
        repository.Add(entity);
    }

    public void Update(int id, Player entity)
    {
        repository.Update(id, entity, (existing, updated) =>
        {
            existing.Name = updated.Name;
            existing.UpdatedAt = DateTime.UtcNow;
        });
    }

    public void Delete(int id)
    {
        repository.Delete(id);
    }
}