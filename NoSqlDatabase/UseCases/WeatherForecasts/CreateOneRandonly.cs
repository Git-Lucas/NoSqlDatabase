using MongoDB.Driver;
using NoSqlDatabase.Data;
using NoSqlDatabase.Models;

namespace NoSqlDatabase.UseCases.WeatherForecasts;

public class CreateOneRandonly(DatabaseContextMongoDb context)
{
    private static readonly string[] _summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
    private readonly IMongoCollection<WeatherForecast> _weatherForecastsDatabase = context
        .GetCollection<WeatherForecast>(DatabaseUtils.GetNameCollection(type: typeof(WeatherForecast)));

    public async Task<Guid> ExecuteAsync()
    {
        WeatherForecast weatherForecast = new(DateOnly.FromDateTime(DateTime.Now),
                                              Random.Shared.Next(-20, 55),
                                              _summaries[Random.Shared.Next(_summaries.Length)]);

        await _weatherForecastsDatabase.InsertOneAsync(weatherForecast);

        return weatherForecast.Id;
    }
}
