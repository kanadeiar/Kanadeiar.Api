using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Kanadeiar.Api.Filters;

/// <summary>
/// Обрботчик исключений -> в статусные коды
/// </summary>
public class ExceptionHandlingAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<ExceptionHandlingAttribute> _logger;

    public ExceptionHandlingAttribute(ILogger<ExceptionHandlingAttribute> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Обработка исключений
    /// </summary>
    /// <param name="context"></param>
    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ArgumentNullException ex:
                _logger.LogCritical(ex, "Отсутствовал аргумент в функции, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new BadRequestObjectResult($"Отсутствовал аргумент в функции, тип: {ex.Message}");
                break;
            case NullReferenceException ex:
                _logger.LogCritical(ex, "Ссылка на null, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                break;
            case ArgumentException ex:
                _logger.LogCritical(ex, "Отсутствовал аргумент в функции, запрос: {0}", context.HttpContext.Request.Path);
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                break;
            case Exception ex:
                _logger.LogCritical(ex, "Критическая ошибка по пути: {0}", context.HttpContext.Request.Path);
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync($"{ex.GetType()}, Message: {ex.Message}");
                break;
        }
    }
}
