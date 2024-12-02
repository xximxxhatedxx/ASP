using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "1a009bc0c5b1ffb3598bbbefb40f55b5";
    private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherInfo> GetWeatherAsync(string city)
    {
        var response = await _httpClient.GetStringAsync($"{BaseUrl}?q={city}&appid={ApiKey}&units=metric");
        var data = JsonConvert.DeserializeObject<dynamic>(response);

        return new WeatherInfo
        {
            City = city,
            Temperature = data.main.temp,
            Description = data.weather[0].description
        };
    }
}
