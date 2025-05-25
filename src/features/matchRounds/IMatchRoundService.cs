using skat_back.features.matchRounds.models;
using skat_back.Lib;

namespace skat_back.Features.MatchRounds;

/// <summary>
///     Represents the service interface for managing match rounds.
///     Specific implementations for the MatchRound Service should be provided here.
/// </summary>
public interface IMatchRoundService : IService<ResponseMatchRoundDto, CreateMatchRoundDto, UpdateMatchRoundDto, int>
{
    // Add specific methods here
}