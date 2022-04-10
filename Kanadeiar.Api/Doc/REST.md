# Памятка по REST-сервисам

## Основное

Начальные шаги:

1. В пустой проект в файл Program зарегистрировать необходимые сервисы:

```sharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
```

2. В этом же файле сконфигурировать конвейер запросов:

```sharp
app.MapControllers();
```

3. Написать обработчик запросов - контроллер:

```sharp
[ApiController]
[Route("api/test")]
public class ValuesController : ControllerBase
{
}
```

## Напоминалки:

Не забывать включить анализатор:
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

Не забывать регистрировать используемые в конечных точках контроллеров сервисы в зависимостях

Добавить пакет и зарегать сериализатор:

```sharp
пакет: Microsoft.AspNetCore.Mvc.NewtonsoftJson
services.AddControllers().AddNewtonsoftJson();
```

Включить составление документации для главного проекта "info.xml" и библиотеки с сущностями "domain.xml", загерать их в Swagger

Разрешить запросы с других доменов:

```xml
services.AddCors();
app.UseCors(builder => builder.AllowAnyOrigin());
```

Образец тестовой конечной точки:
```csharp
[HttpGet("{count}")]
[SwaggerOperation(Summary = "Получить сотрудников", Description = "Получить сотрудников в нужном количестве")]
[SwaggerResponse(StatusCodes.Status200OK, "Сотрудники", Type = typeof(IEnumerable<Person>))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
[SwaggerResponse(StatusCodes.Status404NotFound, "Не найден")]
public async Task<IActionResult> GetAllValues(int count)
{
    if (count == 0)
        return NotFound();
    _logger.LogInformation("Получение тестовых данных");
    var persons = await _personService.GetPersons(count);
    return Ok(persons);
}
```

Образец запроса клиента:
```csharp
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
var response = await httpClient.GetAsync($"/api/test/{10}");
var persons = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<Person>>();
```

Образцовые REST сервис и тестовый клиент - в папке с образцами.

Подключить базу данных и накатить миграции

Настроить валидацию

Настроить маппинг моделей в дтошки и обратно

Подключить логирование

Добавить обработку ошибок

Настроить использование медиатора