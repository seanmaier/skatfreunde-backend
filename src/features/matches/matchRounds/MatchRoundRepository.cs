using skat_back.data;
using skat_back.features.matches.matchRounds.models;
using skat_back.Lib;

namespace skat_back.features.matches.matchRounds;

public class MatchRoundRepository(AppDbContext context) : Repository<MatchRound>(context), IMatchRoundRepository
{
}