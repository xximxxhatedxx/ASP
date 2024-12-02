using Microsoft.AspNetCore.Mvc;

public class WeatherController : Controller
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<IActionResult> Index(string city = "Kyiv")
    {
        var weather = await _weatherService.GetWeatherAsync(city);
        return View(weather);
    }
}
