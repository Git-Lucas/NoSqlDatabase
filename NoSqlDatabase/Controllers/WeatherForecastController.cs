using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using NoSqlDatabase.Data;

namespace NoSqlDatabase.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(DatabaseContextMongoDb context) : ControllerBase
{
    private readonly DatabaseContextMongoDb _context = context;
    private static readonly string[] Summaries = [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("GetDatabasesAndCollections")]
    public async Task<IActionResult> GetDatabasesAndCollectionsAsync()
    {
        Dictionary<string, List<string>> result = await _context.GetDatabasesAndCollectionsAsync();

        return Ok(result);
    }

    [HttpGet("GetDocument")]
    public async Task<IActionResult> GetDatabasesAndCollections([FromQuery] string databaseName, [FromQuery] string collectionName)
    {
        BsonDocument result = await _context.GetDocumentAsync(databaseName, collectionName);

        return Ok(result.ToString());
    }
}
