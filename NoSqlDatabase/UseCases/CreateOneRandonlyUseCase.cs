using NoSqlDatabase.Models;

namespace NoSqlDatabase.UseCases;

public class CreateOneRandonlyUseCase
{
    private static readonly string[] _summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    public Guid Execute()
    {
        WeatherForecast weatherForecast = new(DateOnly.FromDateTime(DateTime.Now),
                                              Random.Shared.Next(-20, 55),
                                              _summaries[Random.Shared.Next(_summaries.Length)]);

        return weatherForecast.Id;
    }
}
