using testecicd.Data;
using Xunit;

namespace testecicd.Tests.Data;

public class WeatherForecastServiceTests
{
    private readonly WeatherForecastService _service = new();

    [Fact]
    public async Task GetForecastAsyncReturnsFiveForecasts()
    {
        var forecasts = await _service.GetForecastAsync(DateTime.Today);

        Assert.Equal(5, forecasts.Length);
    }

    [Fact]
    public async Task GetForecastAsyncReturnsConsecutiveDatesAfterStartDate()
    {
        var startDate = new DateTime(2026, 1, 1);

        var forecasts = await _service.GetForecastAsync(startDate);

        Assert.Equal(
            Enumerable.Range(1, 5).Select(day => startDate.AddDays(day)),
            forecasts.Select(forecast => forecast.Date));
    }

    [Fact]
    public async Task GetForecastAsyncReturnsTemperaturesWithinExpectedRange()
    {
        var forecasts = await _service.GetForecastAsync(DateTime.Today);

        Assert.All(forecasts, forecast => Assert.InRange(forecast.TemperatureC, -20, 54));
    }

    [Fact]
    public async Task GetForecastAsyncReturnsKnownSummaries()
    {
        var forecasts = await _service.GetForecastAsync(DateTime.Today);
        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild",
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        Assert.All(forecasts, forecast => Assert.Contains(forecast.Summary!, summaries));
    }
}
