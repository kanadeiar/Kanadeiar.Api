# Памятка по REST-сервису

## Основное

> Не забывать включить анализатор:
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```
> Не забывать регистрировать сервисы в зависимостях

> Добавить пакет и зарегать сериализатор

```sharp
пакет: Microsoft.AspNetCore.Mvc.NewtonsoftJson
services.AddControllers().AddNewtonsoftJson();
```

> Включить составление документации для главного проекта "info.xml" и библиотеки с сущностями "domain.xml", загерать их в Swagger

Образец тестовой конечной точки:
```csharp
[HttpGet("{count}")]
[SwaggerOperation(Summary = "Получить сотрудников", Description = "Получить сотрудников в нужном количестве")]
[SwaggerResponse(StatusCodes.Status200OK, "Сотрудники", Type = typeof(IEnumerable<Person>))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
public async Task<IActionResult> GetAllValues(int count)
{
    _logger.LogInformation("Получение тестовых данных");
    var persons = await _personService.GetPersons(count);
    return Ok(persons);
}
```

Образцовые REST сервис и тестовый клиент - в папке с образцами.

