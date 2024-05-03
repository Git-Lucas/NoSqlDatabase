namespace NoSqlDatabase.Models;

public class WeatherForecast(DateOnly date, int temperatureC, string summary) : BaseEntity
{
    public DateOnly Date { get; private set; } = date;
    public int TemperatureC { get; private set; } = temperatureC;
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    public string Summary { get; private set; } = summary;
}
