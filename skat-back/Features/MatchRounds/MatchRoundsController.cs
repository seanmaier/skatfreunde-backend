using skat_back.dto.MatchRoundDto;
using skat_back.services.MatchRoundService;

namespace skat_back.controllers;

public class MatchRoundsController(IMatchRoundService service)
    : BaseController<ResponseMatchRoundDto, CreateMatchRoundDto, UpdateMatchRoundDto, int, IMatchRoundService>(
        service);