using Microsoft.AspNetCore.Mvc;
using skat_back.features.statistics.models;

namespace skat_back.features.statistics;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("annualData")]
    public async Task<IActionResult> GetAnnualData([FromQuery] AnnualDataQuery query)
    {
        var annualData = await statisticsService.GetAnnualData(query);

        if (annualData is null)
            return NotFound($"No annual data found for the specified year {query.RequestYear.Year.ToString()}.");

        return Ok(annualData);
    }

    [HttpGet("matchSessions")]
    public async Task<IActionResult> GetMatchSession([FromQuery] MatchSessionQuery query)
    {
        var matchSession = await statisticsService.GetMatchSession(query);

        if (matchSession is null)
            return NotFound($"No match session data found for the specified date {query.WeekStart:d}.");

        return Ok(matchSession);
    }
}