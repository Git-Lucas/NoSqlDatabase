namespace NoSqlDatabase.Models;

public class WeatherForecast : BaseEntity
{
    public DateOnly Date { get; private set; }
    public int TemperatureC { get; private set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string Summary { get; private set; } = string.Empty;

    public WeatherForecast(DateOnly date, int temperatureC, string summary)
    {
        Id = Guid.NewGuid();
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }

    public WeatherForecast() { }
}
