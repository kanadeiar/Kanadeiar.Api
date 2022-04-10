using Kanadeiar.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApiTest1.Models;
using WebApiTest1.Services;

namespace WebApiTest1.Controllers;

[ApiController]
[Route("api/test")]
[ExceptionHandling]
public class ValuesController : ControllerBase
{
    private readonly PersonService _personService;
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(PersonService personService, ILogger<ValuesController> logger)
    {
        _personService = personService;
        _logger = logger;
    }

    [HttpGet("{count}")]
    [SwaggerOperation(Summary = "Получить сотрудников", Description = "Получить сотрудников в нужном количестве")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сотрудники", Type = typeof(IEnumerable<Person>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
    public async Task<IActionResult> GetAllValues(int count)
    {
        _logger.LogInformation("Получение тестовых данных");
        var persons = await _personService.GetPersons(count);
        return Ok(persons);
    }
}
