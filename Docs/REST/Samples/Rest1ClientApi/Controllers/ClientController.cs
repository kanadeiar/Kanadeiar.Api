namespace Rest1ClientApi.Controllers;

[Route("client")]
[ApiController]
[KndExceptionHandling]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;
    public ClientController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить данные по клиенту", Description = "Получить данные по одному клиенту по его идентификатору")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные одного клиента", Type = typeof(ClientDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Не найдено")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Get(int id)
    {
        if (await _mediator.Send(new GetClientById(id), HttpContext.RequestAborted) is { } entity)
        {
            return Ok(entity);
        }
        return NotFound();
    }
}
