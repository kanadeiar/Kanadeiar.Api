namespace RestServer1.Controllers;

[ApiController]
[Route("api/test")]
[ExceptionHandling]
public class ValuesController : ControllerBase
{
    private readonly GenPersonService _genPerson;

    public ValuesController(GenPersonService genPerson)
    {
        _genPerson = genPerson;
    }

    [HttpGet("{count}")]
    [SwaggerOperation(Summary = "Получить сотрудников", Description = "Получить сотрудников в нужном количестве")]
    [SwaggerResponse(StatusCodes.Status200OK, "Сотрудники", Type = typeof(IEnumerable<PersonDto>))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
    public async Task<IActionResult> GetAllValues(int count)
    {
        var persons = await _genPerson.GetPersons(count);
        return Ok(persons);
    }
}
