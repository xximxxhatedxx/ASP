using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TimeController : ControllerBase
{
    private readonly TimeService _timeAnalysisService;

    public TimeController(TimeService timeAnalysisService)
    {
        _timeAnalysisService = timeAnalysisService;
    }

    [HttpGet("currenttime")]
    public IActionResult GetCurrentTimePeriod()
    {
        return Ok(_timeAnalysisService.GetCurrentTimePeriod());
    }
}
