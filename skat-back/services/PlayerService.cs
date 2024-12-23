using skat_back.controllers;
using skat_back.data;
using SQLitePCL;

namespace skat_back.services;

public class PlayerService: IService<Player>
{
    private readonly Repository<Player> _repository;

    public PlayerService(Repository<Player> repository)
    {
        _repository = repository;
    }
    
    public Player? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Player> GetAll()
    {
        return _repository.GetAll();
    }
    
    public void Add(Player player)
    {
        _repository.Add(player);
    }

    public void Update(int id, Player updatedPlayer)
    {
        _repository.Update(id, updatedPlayer, (existing, updated) =>
        {
            existing.Name = updated.Name;
            existing.UpdatedAt = updated.UpdatedAt;
        });
    }

    public void Delete(int id)
    {
        _repository.Delete(id);
    }
}