# Библиотека полезностей :ok_hand:

[Назад](./../README.md)

## Быстрый старт :rocket:

Установить базовый NuGet пакет командой:
```sharp
dotnet add package Kanadeiar.Core
```

Установить NuGet пакет командой:
```sharp
dotnet add package Kanadeiar.Api
```

### Swagger

Удобный интерфейс пользователя для работы с REST API сервисами.

[Интрукции по Swagger](./Docs/Swagger.md).

### Mapster

Удобный маппер моделек

[Интрукции по Swagger](./Docs/Mapster.md).

### Фильтры

Свой удобный фильр для обработки исключений -> в коды ошибок

Использование фильра в API-контроллере:
```sharp
[KarExceptionHandling]
public class ValuesController : ControllerBase
```

