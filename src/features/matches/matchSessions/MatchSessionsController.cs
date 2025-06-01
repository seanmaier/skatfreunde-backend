using skat_back.features.matches.matchSessions.models;
using skat_back.Lib;

namespace skat_back.features.matches.matchSessions;

/// <summary>
///     Represents the API controller for managing match sessions.
/// </summary>
/// <param name="service">The injected MatchSession Service</param>
public class MatchSessionsController(IMatchSessionService service)
    : BaseController<ResponseMatchSessionDto, CreateMatchSessionDto, UpdateMatchSessionDto, int, IMatchSessionService>(
        service);