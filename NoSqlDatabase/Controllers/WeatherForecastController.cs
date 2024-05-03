using Microsoft.AspNetCore.Mvc;
using NoSqlDatabase.DTOs;
using NoSqlDatabase.Models;
using NoSqlDatabase.UseCases.WeatherForecasts;

namespace NoSqlDatabase.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpPost("OneRandonly")]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateOneRandonlyAsync([FromServices] CreateOneRandonly useCase)
    {
        Guid createdId = await useCase.ExecuteAsync();

        return Created(string.Empty, createdId);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetPagedResponse<WeatherForecast>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync([FromServices] GetAll useCase, [FromQuery] int skip = 0, [FromQuery] int take = 5)
    {
        GetPagedResponse<WeatherForecast> weatherForecastsFromDatabase = await useCase.ExecuteAsync(skip, take);

        return Ok(weatherForecastsFromDatabase);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WeatherForecast), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([FromServices] GetById useCase, [FromRoute] Guid id)
    {
        WeatherForecast weatherForecastFromDatabase = await useCase.ExecuteAsync(id);

        return Ok(weatherForecastFromDatabase);
    }
}
