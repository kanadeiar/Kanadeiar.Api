# REST API

[Назад](./../../README.md)

## Главное

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

Не забывать включить Api анализатор в файле cproj:
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

Не забывать регистрировать используемые в конечных точках контроллеров сервисы в зависимостях

Включить составление документации для главного проекта "*.xml" и библиотеки с сущностями "**.xml", загерать их в Swagger

Разрешить запросы с других доменов:

```xml
services.AddCors();
app.UseCors(builder => builder.AllowAnyOrigin());
```

### Сериализатор

Добавить пакет 
```charp
пакет: Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
Зарегать сериализатор в контроллерах:

```sharp
services.AddControllers().AddNewtonsoftJson();
```

### Базовые образцы

Серверная часть:
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

Клиентская часть:
```csharp
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
var response = await httpClient.GetAsync($"/api/test/{10}");
var persons = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<Person>>();
```
