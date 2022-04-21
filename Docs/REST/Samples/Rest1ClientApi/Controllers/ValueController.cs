namespace ClientApi.Controllers;

[Route("value")]
[ApiController]
[KndExceptionHandling]
public class ValueController : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Получить значение", Description = "Получить ответ значение - ответ на запрос")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ответ от сервера", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
    public string Value(string value)
    {
        return $"Hello, {value}!";
    }
}
