using Microsoft.AspNetCore.Mvc;

namespace skat_back.features.statistics;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController(IStatisticsService statisticsService) : ControllerBase
{
    [HttpGet("gameShare")]
    public IActionResult GetGameShare()
    {
        // Placeholder for actual game share logic
        return new JsonResult(new { message = "Game share statistics are not yet implemented." });
    }
    
    [HttpGet("annualData/{year:int}")]
    public async Task<IActionResult> GetAnnualData(int year)
    {
        if (year < 2000 || year > DateTime.Now.Year)
        {
            return BadRequest("Year must be between 2000 and the current year.");
        }

        var annualData = await statisticsService.GetAnnualData(year);

        return Ok(annualData);
    }
}