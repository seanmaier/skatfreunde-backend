using skat_back.controllers;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the API controller for managing match rounds.
/// </summary>
/// <param name="service">The injected MatchRound service</param>
public class MatchRoundsController(IMatchRoundService service)
    : BaseController<ResponseMatchRoundDto, CreateMatchRoundDto, UpdateMatchRoundDto, int, IMatchRoundService>(
        service);