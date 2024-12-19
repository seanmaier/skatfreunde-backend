using Microsoft.EntityFrameworkCore;
using skat_back.data;

namespace skat_back.services;

public class MatchdayService
{
    private readonly AppDbContext _context;

    public MatchdayService(AppDbContext context)
    {
        _context = context;
    }

    public List<Matchday> GetAllMatchdays()
    {
        return _context.Matchdays.Include(m => m.Matches).ToList();
    }

    public Matchday? GetMatchById(int id)
    {
        return _context.Matchdays.Include(m => m.Matches).FirstOrDefault(m => m.Id == id);
    }

    public void AddMatchday(Matchday matchday)
    {
        _context.Matchdays.Add(matchday);
        _context.SaveChanges();
    }

    public void UpdateMatchday(int id, Matchday updated)
    {
        var matchday = _context.Matchdays.Find(id);
        if (matchday != null)
        {
            matchday.Date = updated.Date;
            matchday.Matches = updated.Matches;
            _context.SaveChanges();
        }
    }

    public void DeleteMatchday(int id)
    {
        var matchday = _context.Matchdays.Find(id);
        if (matchday != null)
        {
            _context.Matchdays.Remove(matchday);
            _context.SaveChanges();
        }
    }

    public Player? GetPlayerById(int id)
    {
        return _context.Players.Find(id);
    }
}