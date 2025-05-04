using skat_back.data;
using skat_back.models;

namespace skat_back.services.MatchSessionService;

public class MatchSessionService(AppDbContext db): IMatchSessionService
{
    public IEnumerable<MatchSession> GetAll()
    {
        return db.MatchSessions.ToList();
    }

    public MatchSession? GetById(string id)
    {
        return db.MatchSessions.Find(id);
    }

    public void Add(MatchSession matchSession)
    {
        db.Add(matchSession);
        db.SaveChanges();
    }

    public void Update(string id, MatchSession updatedMatchSession)
    {
        var existingMatchSession = db.MatchSessions.Find(id);
        if (existingMatchSession == null)
            throw new Exception("MatchSession not found");

        existingMatchSession.DateOfTheWeek = updatedMatchSession.DateOfTheWeek;
        db.SaveChanges();
    }

    public void Delete(string id)
    {
        var matchSession = db.MatchSessions.Find(id);
        if (matchSession == null)
            throw new Exception("MatchSession not found");
        db.MatchSessions.Find(matchSession);
    }
}