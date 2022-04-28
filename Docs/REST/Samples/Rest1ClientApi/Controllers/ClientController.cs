namespace Rest1ClientApi.Controllers;

/// <summary>
/// Клиенты
/// </summary>
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

    /// <summary>
    /// Получение элементов со смещением и количеством
    /// </summary>
    /// <param name="offset">смещение</param>
    /// <param name="count">количество</param>
    /// <returns>Асинхронная коллекция</returns>
    [HttpGet()]
    [SwaggerOperation(Summary = "Получить множество элементов-клиентов", Description = "Получить данные по множству клиентов по смещению и количеству")]
    [SwaggerResponse(StatusCodes.Status200OK, "Данные множества клиентов", Type = typeof(IAsyncEnumerable<ClientDto>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async IAsyncEnumerable<ClientDto> GetPaged(int offset, int count)
    {
        await foreach (var item in _mediator.CreateStream(new GetPagedClient(offset, count), HttpContext.RequestAborted))
        {
            yield return item;
        }
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
        if (await _mediator.Send(new GetClientById(id), HttpContext.RequestAborted) is { } entity)
        {
            return Ok(entity);
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
        var result = await _mediator.Send(new AddUpdateClient(0, dto), HttpContext.RequestAborted);
        return Ok(result);
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
        if (await _mediator.Send(new AddUpdateClient(id, dto), HttpContext.RequestAborted) > 0)
        {
            return Ok(true);
        }
        return NotFound(false);
    }

    /// <summary>
    /// Изменить один элемент
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="patch">Изменения элемента</param>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     [
    ///         { "op": "test", "path": "property_name", "value": "value" },
    ///         { "op": "remove", "path": "property_name" },
    ///         { "op": "add", "path": "property_name", "value": [ "value1", "value2" ] },
    ///         { "op": "replace", "path": "property_name", "value": 12 },
    ///         { "op": "move", "from": "property_name", "path": "other_property_name" },
    ///         { "op": "copy", "from": "property_name", "path": "other_property_name" }
    ///     ]
    /// 
    /// </remarks>
    /// <returns>Успешность</returns>
    [HttpPatch("{id}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation(Summary = "Изменить один элемент", Description = "Произвести изменения в элементе согласно запроса")]
    [SwaggerResponse(StatusCodes.Status200OK, "Успешное изменение", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Не найдено", Type = typeof(bool))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Плохой запрос", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ошибка на сервере", Type = typeof(string))]
    public async Task<IActionResult> Change(int id, [FromBody] JsonPatchDocument<Client> patch)
    {
        if (await _mediator.Send(new PatchClient(id, patch), HttpContext.RequestAborted))
        {
            return Ok(true);
        }
        return NotFound(false);
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
        if (await _mediator.Send(new DeleteClient(id), HttpContext.RequestAborted))
        {
            return Ok(true);
        }
        return NotFound(false);
    }
}
