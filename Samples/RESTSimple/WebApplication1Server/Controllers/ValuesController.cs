using Kanadeiar.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApplication1Server.Models;

namespace WebApplication1Server.Controllers;

[ApiController]
[Route("api/test")]
[ExceptionHandling]
public class ValuesController : ControllerBase
{
    public ValuesController()
    {
    }

    [HttpGet("{count}")]
    [SwaggerOperation(Summary = "Получить сотрудников", Description = "Получить сотрудников в нужном количестве")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сотрудники", Type = typeof(IEnumerable<Person>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
    public async Task<IActionResult> GetAllValues(int count)
    {
        await Task.Delay(1000);
        var persons = Enumerable.Range(0, count).Select(i => new Person
        {
            Id = i,
            Surname = $"Иванов_{i}",
            Firstname = $"Иван_{i}",
            Age = 18 + i,
        }).ToArray();
        return Ok(persons);
    }
}
