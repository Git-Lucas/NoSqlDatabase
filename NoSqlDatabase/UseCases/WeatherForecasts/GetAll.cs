using MongoDB.Driver;
using NoSqlDatabase.Data;
using NoSqlDatabase.DTOs;
using NoSqlDatabase.Models;

namespace NoSqlDatabase.UseCases.WeatherForecasts;

public class GetAll(DatabaseContextMongoDb context)
{
    private readonly IMongoCollection<WeatherForecast> _weatherForecastsDatabase = context.GetCollection<WeatherForecast>();

    public async Task<GetPagedResponse<WeatherForecast>> ExecuteAsync(int skip, int take)
    {
        IEnumerable<WeatherForecast> weatherForecasts = await _weatherForecastsDatabase
            .Find(x => true)
            .Skip(skip)
            .Limit(take)
            .ToListAsync();

        long countDataInDatabase = await _weatherForecastsDatabase.EstimatedDocumentCountAsync();

        GetPagedResponse<WeatherForecast> weatherForecastsPaged = new(countDataInDatabase, 
                                                                      skip, 
                                                                      take, 
                                                                      weatherForecasts);

        return weatherForecastsPaged;
    }
}
