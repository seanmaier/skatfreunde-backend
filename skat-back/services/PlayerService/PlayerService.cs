using skat_back.controllers;
using skat_back.data;

namespace skat_back.services.PlayerService;

public class PlayerService : IPlayerService
{
    private readonly IRepository<Player> _repository;

    public PlayerService(IRepository<Player> repository)
    {
        _repository = repository;
    }

    public IEnumerable<Player> GetAll()
    {
        return _repository.GetAll();
    }

    public Player? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Add(Player entity)
    {
        _repository.Add(entity);
    }

    public void Update(int id, Player entity)
    {
        _repository.Update(id, entity, (existing, updated) =>
        {
            existing.Name = updated.Name;
            existing.UpdatedAt = DateTime.UtcNow;
        });
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}