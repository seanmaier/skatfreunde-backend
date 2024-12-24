using skat_back.data;
using skat_back.services.MatchService;

namespace skat_back.controllers;

public class MatchController: BaseController<Match, MatchService>
{
    public MatchController(MatchService service): base(service){}
}