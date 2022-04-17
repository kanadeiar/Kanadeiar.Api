using Kanadeiar.Api.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace RestService1.Controllers;

[Route("value")]
[ApiController]
[KarExceptionHandling]
public class ValueController : ControllerBase
{
    public ValueController() 
    { }

    [HttpGet]
    [SwaggerOperation(Summary = "Получить значение", Description = "Получить ответ значение - ответ на запрос")]
    [SwaggerResponse(StatusCodes.Status200OK, "Ответ от сервера", Type = typeof(string))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
    public string Value(string value)
    {
        return $"Hello, {value}!";
    }
}
