# REST API

[Назад](./../../README.md)

## Главное

Начальные шаги:

```sharp
dotnet new web
```

1. В пустой проект в файл Program зарегистрировать необходимые сервисы:

```sharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
```

2. В этом же файле сконфигурировать конвейер запросов:

```sharp
app.MapControllers();
```

3. Написать обработчик запросов - API-контроллер в папку "Controllers":

```sharp
[ApiController]
[Route("value")]
public class ValueController : ControllerBase
{
}
```

4. Переиспользовать Swagger, Mapster, KarErrorHandler из библиотеки.

5. Настроить запуск приложения:
```sharp
"launchUrl": "swagger",
"applicationUrl": "https://localhost:6001;http://localhost:6000",
```

## Напоминалки:

Не забывать включить Api анализатор в файле cproj в раздел "PropertyGroup":
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

Не забывать регистрировать используемые в конечных точках контроллеров сервисы в зависимостях

Включить составление документации для главного проекта "*.xml" и библиотеки с сущностями "**.xml", загерать их в Swagger

Разрешить запросы с других доменов:

```xml
services.AddCors(); //в вервисах
app.UseCors(builder => builder.AllowAnyOrigin()); //в конвейере после роутинга перед контроллерами
```

### Сериализатор

Добавить пакет 
```sharp
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
Зарегать сериализатор в контроллерах:

```sharp
services.AddControllers().AddNewtonsoftJson();
```

### Простые образцы

Серверная часть:
```csharp
[HttpGet]
[SwaggerOperation(Summary = "Получить значение", Description = "Получить ответ значение - ответ на запрос")]
[SwaggerResponse(StatusCodes.Status200OK, "Ответ от сервера", Type = typeof(string))]
[SwaggerResponse(StatusCodes.Status500InternalServerError, "Плохой запрос", Type = typeof(string))]
public string Value(string value)
{
    return $"Hello, {value}!";
}
```

Клиентская часть:
```csharp
HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
var response = await httpClient.GetAsync($"/value?value=Test");
var message = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
```
