using Microsoft.EntityFrameworkCore;
using skat_back.data;

namespace skat_back.services;

public class MatchDayService
{
    private readonly AppDbContext _context;

    public MatchDayService(AppDbContext context)
    {
        _context = context;
    }

    public List<Matchday> GetAllMatchdays()
    {
        return _context.MatchDays.Include(m => m.Matches).ToList();
    }

    public Matchday? GetMatchById(int id)
    {
        return _context.MatchDays.Include(m => m.Matches).FirstOrDefault(m => m.Id == id);
    }

    public void AddMatchday(Matchday matchDay)
    {
        _context.MatchDays.Add(matchDay);
        _context.SaveChanges();
    }

    public void UpdateMatchday(int id, Matchday updated)
    {
        var matchDay = _context.MatchDays.Find(id);
        if (matchDay != null)
        {
            matchDay.Date = updated.Date;
            matchDay.Matches = updated.Matches;
            _context.SaveChanges();
        }
    }

    public void DeleteMatchday(int id)
    {
        var matchDay = _context.MatchDays.Find(id);
        if (matchDay != null)
        {
            _context.MatchDays.Remove(matchDay);
            _context.SaveChanges();
        }
    }

    public Player? GetPlayerById(int id)
    {
        return _context.Players.Find(id);
    }
}