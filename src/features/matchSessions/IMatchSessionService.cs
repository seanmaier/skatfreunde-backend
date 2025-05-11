using skat_back.services;

namespace skat_back.Features.MatchSessions;

/// <summary>
///     Represents the service interface for managing match sessions.
///     Specific implementations for the MatchSession Service should be provided here.
/// </summary>
public interface
    IMatchSessionService : IService<ResponseMatchSessionDto, CreateMatchSessionDto, UpdateMatchSessionDto, int>
{
    // Add specific methods here
}