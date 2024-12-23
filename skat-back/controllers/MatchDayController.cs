using skat_back.data;
using skat_back.services;

namespace skat_back.controllers;

public class MatchDayController : BaseController<MatchDay, MatchDayService>
{
    public MatchDayController(MatchDayService service) : base(service)
    {
    }
}