using EtherpunkBlazorWasm.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EtherpunkBlazorWasm.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
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

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)],
			UserName = User.Identity?.Name ?? "No User"
		})

		.ToArray();
	}

	[HttpGet("today"), Authorize]
	public IEnumerable<WeatherForecast> Today()
	{
		var rng = new Random();
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)],
			UserName = User.Identity?.Name ?? "No User"
		});
	}

	[HttpGet("tomorrow"), Authorize(Roles = "Admin")]
	public IEnumerable<WeatherForecast> Tomorrow()
	{
		var rng = new Random();
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)],
			UserName = User.Identity?.Name ?? "No User"
		});
	}
}