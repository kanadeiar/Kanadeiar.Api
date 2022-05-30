namespace MT1Gateway.Controllers;

/// <summary>
/// Клиенты
/// </summary>
[Route("client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IBus _bus;
    public ClientController(IBus bus)
    {
        _bus = bus;
    }

    /// <summary>
    /// Получение элементов со смещением и количеством
    /// </summary>
    /// <param name="offset">смещение</param>
    /// <param name="count">количество</param>
    /// <returns>Коллекция</returns>
    [HttpGet()]
    [SwaggerOperation(Summary = "Получить множество элементов-клиентов", Description = "Получить данные по множству клиентов по смещению и количеству")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные множества клиентов", Type = typeof(IEnumerable<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IEnumerable<ClientDto>> GetPaged(int offset, int count)
    {
        var client = _bus.CreateRequestClient<GetPagedClientQuery>();
        var ok = await client.GetResponse<GetPagedClientQuery.IOk>(new GetPagedClientQuery(offset, count));
        var dtos = ok.Message.Clients.Adapt<IEnumerable<ClientDto>>();
        return dtos;
    }

    /// <summary>
    /// Получение количества элементов
    /// </summary>
    /// <returns>Количество</returns>
    [HttpGet("count")]
    [SwaggerOperation(Summary = "Получить количество элементов", Description = "Получить данные по количеству элементов")]
    [SwaggerResponse(StatusCodes.Status200OK, "Количество", Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<int> GetCount()
    {
        var client = _bus.CreateRequestClient<GetClientCountQuery>();
        var ok = await client.GetResponse<GetClientCountQuery.IOk>(new GetClientCountQuery());
        return ok.Message.Count;
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
        var client = _bus.CreateRequestClient<GetClientByIdQuery>();
        var (ok, _) = await client.GetResponse<GetClientByIdQuery.IOk, GetClientByIdQuery.INotFound>(new GetClientByIdQuery(id));
        if (ok.IsCompletedSuccessfully)
        {
            var item = (await ok).Message.Client;
            return Ok(item.Adapt<ClientDto>());
        }
        return NotFound();
    }
}
