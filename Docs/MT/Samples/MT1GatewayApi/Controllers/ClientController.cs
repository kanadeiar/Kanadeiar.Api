using MT1ClientDomain.Entites;

namespace MT1Gateway.Controllers;

/// <summary>
/// Клиенты
/// </summary>
[Route("client")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IBus _bus;
    private readonly IPublishEndpoint _endpoint;
    public ClientController(IBus bus, IPublishEndpoint endpoint)
    {
        _bus = bus;
        _endpoint = endpoint;
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

    /// <summary>
    /// Добавить один элемент
    /// </summary>
    /// <param name="dto">данные элемента</param>
    /// <returns>Новый идентификатор</returns>
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Добавить один элемент", Description = "Добавить данные по одному клиенту с получением его идентификатора")]
    [SwaggerResponse(StatusCodes.Status200OK, "Полученный идентификатор", Type = typeof(int))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Add([FromBody] ClientDto dto)
    {
        var client = _bus.CreateRequestClient<AddClientCommand>();
        var ok = await client.GetResponse<AddClientCommand.IOk>(new AddClientCommand(dto.Adapt<Client>()));
        return Ok(ok.Message.Id);
    }

    /// <summary>
    /// Обновить один элемент
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <returns>Успешность</returns>
    [HttpPut("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Обновить один элемент", Description = "Обновить данные по одному клиенту по его идентификатору")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешность обновления", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Не найдено", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Update(int id, [FromBody] ClientDto dto)
    {
        var client = _bus.CreateRequestClient<UpdateClientCommand>();
        var ok = await client.GetResponse<UpdateClientCommand.IOk>(new UpdateClientCommand(id, dto.Adapt<Client>()));
        return Ok(ok.Message.Success);
    }

    /// <summary>
    /// Удалить элемент
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Успешность</returns>
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Удалить элемент", Description = "Удалить элемент с определенным идентификатором")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешное удаление", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Не найдено", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _bus.CreateRequestClient<DeleteClientCommand>();
        var ok = await client.GetResponse<DeleteClientCommand.IOk>(new DeleteClientCommand(id));
        return Ok(ok.Message.Success);
    }
}
