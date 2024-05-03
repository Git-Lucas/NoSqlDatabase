using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using NoSqlDatabase.Data;
using NoSqlDatabase.Models;
using NoSqlDatabase.UseCases;

namespace NoSqlDatabase.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(CreateOneRandonlyUseCase createOneRandonlyUseCase) : ControllerBase
{
    private readonly CreateOneRandonlyUseCase _createOneRandonlyUseCase = createOneRandonlyUseCase;

    [HttpPost("OneRandonly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public IActionResult CreateOneRandonly()
    {
        Guid createdId = _createOneRandonlyUseCase.Execute();

        return Created(string.Empty, createdId);
    }

    [HttpGet("CountDatabase")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> CountDatabase([FromServices] DatabaseContextMongoDb context)
    {
        IMongoCollection<WeatherForecast> weatherForecastsDatabase = context
            .GetCollection<WeatherForecast>(DatabaseUtils.GetNameCollection(typeof(WeatherForecast)));

        return Ok(await weatherForecastsDatabase.EstimatedDocumentCountAsync());
    }
}
