namespace MT1Gateway.Controllers;

/// <summary>
/// Клиенты
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IBus _bus;
    IRequestClient<GetClientByIdQuery> _clientByIdQuery;
    public ClientController(IBus bus)
    {
        _bus = bus;
        _clientByIdQuery = bus.CreateRequestClient<GetClientByIdQuery>();
    }

    /// <summary>
    /// Получение одного элемента
    /// </summary>
    /// <param name="id">Индекс</param>
    /// <returns>Элемент</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Получить один элемент-клиент", Description = "Получить данные по одному клиенту по его идентификатору")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные одного клиента", Type = typeof(ClientDto))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Не найдено")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Get(int id)
    {
        var (okGet, _) = await _clientByIdQuery.GetResponse<GetClientByIdQuery.IOk, GetClientByIdQuery.INotFound>(new GetClientByIdQuery(id));
        if (okGet.IsCompletedSuccessfully)
        {
            var item = (await okGet).Message.Client;
            return Ok(item.Adapt<ClientDto>());
        }
        return NotFound();
    }
}
