# Маппер моделек Mapster

[Назад](./../README.md)

Установить пакет:
```sharp
dotnet add package Kanadeiar.Api
```

Регистрация в сервисах:
```sharp
builder.Services.KarAddMapster();
```

Примеры использования 
```sharp
var config = new TypeAdapterConfig().ForType<NewsUnitDto, NewsUnit>().Ignore(x => x.Id).Config; //конфигурация
var item = request.Model.Adapt<NewsUnit>(config); //получение нового с учетом конфигурации
request.Model.Adapt(entity, config); //обновление согласно конфигурации, не получение нового объекта
var test = request.Model.Adapt<NewsUnit>(); //получение нового объекта
persons.Adapt<IEnumerable<PersonDto>>(); //коллекция новых объектов
```