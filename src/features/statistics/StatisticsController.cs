using Microsoft.AspNetCore.Mvc;

namespace skat_back.features.statistics;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("annualData/{year:int}")]
    public async Task<IActionResult> GetAnnualData(int year)
    {
        if (year < 2000 || year > DateTime.Now.Year)
            return BadRequest("Year must be between 2000 and the current year."); // TODO add validator

        var annualData = await statisticsService.GetAnnualData(year);

        return Ok(annualData);
    }

    [HttpGet("matchSessions")]
    public async Task<IActionResult> GetMatchSession([FromQuery] DateTime weekStart)
    {
        var matchSession = await statisticsService.GetMatchSession(weekStart);

        return Ok(matchSession);
    }
}