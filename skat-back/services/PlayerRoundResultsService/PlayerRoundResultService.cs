using skat_back.data;
using skat_back.models;

namespace skat_back.services.PlayerRoundResultsService;

public class PlayerRoundResultService(AppDbContext db): IPlayerRoundResultService
{
    public IEnumerable<PlayerRoundResult> GetAll()
    {
        return db.PlayerRoundResults.ToList();
    }

    public PlayerRoundResult? GetById(string id)
    {
        return db.PlayerRoundResults.Find(id);
    }

    public void Add(PlayerRoundResult entity)
    {
        db.Add(entity);
        db.SaveChanges();
    }

    public void Update(string id, PlayerRoundResult entity)
    {
        var existing = db.PlayerRoundResults.Find(id);
        if (existing == null)
            throw new Exception("PlayerRoundResult not found");
        
        existing.PlayerId = entity.PlayerId;
        existing.Won = entity.Won;
        existing.Lost = entity.Lost;
        existing.Points = entity.Points;
        existing.Table = entity.Table;
        existing.UpdatedAt = DateTime.UtcNow;

        db.SaveChanges();
    }

    public void Delete(string id)
    {
        var playerRoundResult = db.PlayerRoundResults.Find(id);
        if (playerRoundResult == null)
            throw new Exception("PlayerRoundResult not found");
        db.PlayerRoundResults.Remove(playerRoundResult);
    }
}