using skat_back.features.matches.matchSessions.models;
using skat_back.features.matchSessions.models;
using skat_back.Lib;

namespace skat_back.features.matches.matchSessions;

/// <summary>
///     Represents the service interface for managing match sessions.
///     Specific implementations for the MatchSession Service should be provided here.
/// </summary>
public interface
    IMatchSessionService : IService<ResponseMatchSessionDto, CreateMatchSessionDto, UpdateMatchSessionDto, int>
{
    // Add specific methods here
}