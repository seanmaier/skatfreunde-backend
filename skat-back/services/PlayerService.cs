using skat_back.data;
using SQLitePCL;

namespace skat_back.services;

public class PlayerService
{
    private readonly Repository<Player> _repository;

    public PlayerService(Repository<Player> repository)
    {
        _repository = repository;
    }
    
    public Player? GetPlayerById(int id)
    {
        return _repository.GetById(id);
    }

    public IEnumerable<Player> GetAllPlayer()
    {
        return _repository.GetAll();
    }
    
    public void AddPlayer(Player player)
    {
        _repository.Add(player);
    }

    public void UpdatePlayer(int id, Player updatedPlayer)
    {
        _repository.Update(id, updatedPlayer, (existing, updated) =>
        {
            existing.Name = updated.Name;
            existing.UpdatedAt = updated.UpdatedAt;
        });
    }

    public void DeletePlayer(int id)
    {
        _repository.Delete(id);
    }
}