using skat_back.data;
using skat_back.features.matchRounds.models;
using skat_back.Lib;

namespace skat_back.Features.MatchRounds;

public class MatchRoundRepository(AppDbContext context) : Repository<MatchRound>(context), IMatchRoundRepository
{
    
}