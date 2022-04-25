# REST API

[Назад](./../../README.md)

## Напоминалки:

Не забывать включить Api анализатор в файле cproj в раздел "PropertyGroup":
```xml
<IncludeOpenAPIAnalyzers>true</IncludeOpenAPIAnalyzers>
```

Не забывать регистрировать используемые в конечных точках контроллеров сервисы в зависимостях


## Главное

Начальные шаги:

```sharp
dotnet new web
```

1. Основа приложения: сущности и интерфейсы - в Domain (ссылка на Kanadeiar.Core), бизнес-логика и общее - в Application (ссылки на Kanadeiar.Api и Domain), База данных и специфическое - в Infrastructure (ссылки на Application), детальная реализация - в Api (ссылки на Infrastructure)

1. В пустой проект в файл Program зарегистрировать необходимые сервисы:

```sharp
builder.Services.AddControllers();
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
4. Разрешить запросы с других доменов:

```xml
services.AddCors(); //в вервисах
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); //в конвейере после роутинга перед контроллерами
```

5. Настроить запуск приложения в файле launchSettings.json:

```sharp
"launchUrl": "swagger",
"applicationUrl": "https://localhost:6001;http://localhost:6000",
```

6. Включить составление документации.

Для главного проекта "*.xml" и библиотеки с дтошками "*.sample.xml", загерать их в Swagger

7. Иcпользовать библиотеку Kanadeiar.Api.

[Документация по библиотеке](./../../Kanadeiar.Api/README.md)

### Если нужно:

Принудительное перенаправление на https:

```csharp
app.UseHttpsRedirection(); //перед app.UseRouting()
```
Уведомление браузеров о https:

```cshapr
app.UseHsts();
```

### База данных EF

[Интрукции по базе данных](./Database.md).

### Репозитории

Добавление слабосвязаности между бизнес-логикой и базой данных

[Интрукции по репозиториям](./Repositories.md).

### Медиатор MediatR

Для использования шаблона CQRS в проекте. Уже включен в Kanadeiar.Api.

[Инструкции по медиатору](./MediatR.md).

### Валидация FluentValidation

Для проверки корректности вводимых данных

[Инструкции по валидации](./FluentValidation.md).

### Сериализатор

Добавить пакет 
```sharp
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```
Зарегать сериализатор в контроллерах:

```sharp
services.AddControllers().AddNewtonsoftJson();
```




### Простые образцы в api-контроллере приложения

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
