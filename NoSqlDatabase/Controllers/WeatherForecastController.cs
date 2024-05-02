using Microsoft.AspNetCore.Mvc;
using NoSqlDatabase.UseCases;

namespace NoSqlDatabase.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(CreateOneRandonlyUseCase createOneRandonlyUseCase) : ControllerBase
{
    private readonly CreateOneRandonlyUseCase _createOneRandonlyUseCase = createOneRandonlyUseCase;

    [HttpPost("OneRandonly")]
    public IActionResult CreateOneRandonly()
    {
        Guid createdId = _createOneRandonlyUseCase.Execute();

        return Created(string.Empty, createdId);
    }
}
