using MongoDB.Driver;
using NoSqlDatabase.Data;
using NoSqlDatabase.Models;

namespace NoSqlDatabase.UseCases;

public class CreateOneRandonlyUseCase(DatabaseContextMongoDb context)
{
    private static readonly string[] _summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];
    private readonly IMongoCollection<WeatherForecast> _weatherForecastsDatabase = context
        .GetCollection<WeatherForecast>(DatabaseUtils.GetNameCollection(typeof(WeatherForecast)));

    public Guid Execute()
    {
        WeatherForecast weatherForecast = new(DateOnly.FromDateTime(DateTime.Now),
                                              Random.Shared.Next(-20, 55),
                                              _summaries[Random.Shared.Next(_summaries.Length)]);

        _weatherForecastsDatabase.InsertOne(weatherForecast);

        return weatherForecast.Id;
    }
}
