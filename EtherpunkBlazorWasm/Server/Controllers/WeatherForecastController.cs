using EtherpunkBlazorWasm.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EtherpunkBlazorWasm.Server.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet, Route("api/weather/thisweek")]
    public IEnumerable<WeatherForecast> Get()
    {
		return GetForecasts();
	}

	[HttpGet, Authorize, Route("api/weather/nextweek")]
	public IEnumerable<WeatherForecast> NextWeek()
	{
		return GetForecasts(7);
	}

	[HttpGet, Authorize(Roles = "Admin"), Route("api/weather/lastWeek")]
	public IEnumerable<WeatherForecast> LastWeek()
	{
		return GetForecasts(-7);
	}

	private IEnumerable<WeatherForecast> GetForecasts(int offset = 0)
	{
		var rng = new Random();
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index + offset)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)],
			UserName = User.Identity?.Name ?? "No User"
		});
	}
}