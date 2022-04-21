using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kanadeiar.Api.Filters;

/// <summary>
/// Обрботчик исключений -> в статусные коды
/// </summary>
public class KndExceptionHandlingAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// Обработка исключений
    /// </summary>
    /// <param name="context"></param>
    public override void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices.GetService<ILogger<KndExceptionHandlingAttribute>>();
        switch (context.Exception)
        {
            case ArgumentNullException ex:
                logger?.LogError(ex, "Отсутствовал аргумент в функции, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new BadRequestObjectResult($"Отсутствовал аргумент в функции, тип: {ex.Message}");
                break;
            case NullReferenceException ex:
                logger?.LogError(ex, "Ссылка на null, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                break;
            case ArgumentException ex:
                logger?.LogError(ex, "Ошибка аргумента в функции, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                break;
            case Exception ex:
                logger?.LogCritical(ex, "Критическая ошибка по пути: {0}", context.HttpContext.Request.Path);
                var result = new ObjectResult(ex.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                };
                context.Result = result;
                break;
        }
    }
}
