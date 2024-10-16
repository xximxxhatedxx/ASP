using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CalcController : ControllerBase
{
    private readonly CalcService _calcService;

    public CalcController(CalcService calcService)
    {
        _calcService = calcService;
    }

    [HttpGet("add")]
    public IActionResult Add(int a, int b)
    {
        return Ok(_calcService.Add(a, b));
    }

    [HttpGet("sub")]
    public IActionResult Sub(int a, int b)
    {
        return Ok(_calcService.Sub(a, b));
    }

    [HttpGet("mul")]
    public IActionResult Mul(int a, int b)
    {
        return Ok(_calcService.Mul(a, b));
    }

    [HttpGet("div")]
    public IActionResult Div(int a, int b)
    {
        try
        {
            return Ok(_calcService.Div(a, b));
        }
        catch (DivideByZeroException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}