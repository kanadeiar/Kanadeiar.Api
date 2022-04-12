# Конфигурирование приложения

## Swagger

Удобный интерфейс пользователя для работы с REST API сервисами.

Регистрация в сервисах:
```sharp
builder.Services.KanadeiarAddSwagger("WebApiTest1");
```

Зарегистрировать использование в конвейере, желательно в самом начале, и в среде разработки приложения:
```sharp
app.UseSwagger();
app.UseSwaggerUI();
```

> Обязательно зарегать их в сервисах для корректной работы: 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

## Mapster

Удобный маппер

Регистрация в сервисах:
```sharp
builder.Services.KanadeiarAddMapster();
```

Использование 
```sharp
var config = new TypeAdapterConfig().ForType<NewsUnitDto, NewsUnit>().Ignore(x => x.Id).Config; //конфигурация
var item = request.Model.Adapt<NewsUnit>(config); //получение нового с учетом конфигурации
request.Model.Adapt(entity, config); //обновление согласно конфигурации, не получение нового объекта
var test = request.Model.Adapt<NewsUnit>(); //получение нового объекта
persons.Adapt<IEnumerable<PersonDto>>(); //коллекция новых объектов
```