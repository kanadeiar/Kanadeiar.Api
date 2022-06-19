# MassTransit + RabbitMQ

[Назад](./../../README.md)

## Главное

Начальные шаги:

```sharp
dotnet new worker
```

0. Основа приложения: сущности и интерфейсы - в Domain (ссылка на Kanadeiar.Core), 
бизнес-логика, контракты (с ответами) и реализация их и общее - в Application (ссылки на Kanadeiar.Api и Domain), 
База данных, тестовые данные и специфическое - в Infra (ссылки на Application), 
Транспортные модели и специфическое HTTP - в GatewayInfra (ссылки на Application),
Реализация микросервиса - Api (ссылки на Infra),
Реализация шлюза - GatewayApi (ссылки на GatewayInfra)

### Микросервис

1. Добавить потребители запросов, реализцющие этот интерфейс, где T - объявленные ранее контракты

```sharp
IConsumer<T>
```

2. Зарегистрировать потребители запросов используя метода-расширение:

```sharp
services.MyMassTransit();
```

3. Добавить базу данных

4. Зарегать репозитории

5. Добавить поддержку тестовых данных

6. Иcпользовать библиотеку Kanadeiar.Api.

[Документация по библиотеке](./../../Kanadeiar.Api/README.md)

### Шлюз.

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

### База данных на ORM Entity Framework

[Интрукции по базе данных](./Database.md).

### Репозитории

Добавление слабосвязаности между бизнес-логикой и базой данных.

Добавить пакет в Domain:
```sharp
dotnet add package Kanadeiar.Core
```
Добавить пакет в Application:
```sharp
dotnet add package Kanadeiar.Api
```

Остальные инструкции смотреть в инструкциях библиотеки.

### Использование ORM Dapper

Добавить пакеты в Application:
```sharp
dotnet add package Dapper
dotnet add package Dapper.Logging
dotnet add package Microsoft.Data.SqlClient
```

Создать специальный репозиторий для работы с использованием ORM Dapper

Зарегистрировать в сервисах приложения:
```sharp
builder.Services.AddDbConnectionFactory(provider => new SqlConnection(builder.Configuration.GetValue<string>("ConnectionString")));
builder.Services.AddScoped<IClientRepository, ClientDapperRepository>();
```

### Начальные тестовые данные

Содержится в библиотеке.

Инструкции смотреть в библиотеке.

### Медиатор MediatR

Для использования шаблона CQRS в проекте. Уже включен в Kanadeiar.Api.

Добавить пакет в Application:
```sharp
dotnet add package Kanadeiar.Api
```

Остальные инструкции смотреть в инструкциях библиотеки.

### Валидация FluentValidation

Для проверки корректности вводимых данных.

[Инструкции по валидации](./FluentValidation.md).

### Лог Serilog

Удобное логирование работы приложения.

[Инструкции по логированию](./Serilog.md).

### HTTP Json Клиент

Клиент - оболочка над стандартным клиентом

Добавить пакет в приложение:
```sharp
dotnet add package Kanadeiar.Api
```
Остальные инструкции смотреть в инструкциях библиотеки.




