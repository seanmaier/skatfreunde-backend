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

    public List<MatchDay> GetAllMatchDays()
    {
        return _context.MatchDays.Include(m => m.Matches).ToList();
    }

    public MatchDay? GetMatchById(int id)
    {
        return _context.MatchDays.Include(m => m.Matches).FirstOrDefault(m => m.Id == id);
    }

    public void AddMatchDay(MatchDay matchDay)
    {
        _context.MatchDays.Add(matchDay);
        _context.SaveChanges();
    }

    public void UpdateMatchDay(int id, MatchDay updated)
    {
        var matchDay = _context.MatchDays.Find(id);
        if (matchDay != null)
        {
            matchDay.Date = updated.Date;
            matchDay.Matches = updated.Matches;
            _context.SaveChanges();
        }
    }

    public void DeleteMatchDay(int id)
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