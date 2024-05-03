using MongoDB.Driver;
using NoSqlDatabase.Data;
using NoSqlDatabase.Models;

namespace NoSqlDatabase.UseCases.WeatherForecasts;

public class GetById(DatabaseContextMongoDb context)
{
    private readonly IMongoCollection<WeatherForecast> _weatherForecasts = context.GetCollection<WeatherForecast>();

    public async Task<WeatherForecast> ExecuteAsync(Guid id)
    {
        WeatherForecast weatherForecastFromDatabase = await _weatherForecasts.Find(x => x.Id == id).FirstOrDefaultAsync();

        return weatherForecastFromDatabase;
    }
}
