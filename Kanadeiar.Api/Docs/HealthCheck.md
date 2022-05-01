# Проверка работоспособности микросервиса

[Назад](./../README.md)

Установить пакет:
```sharp
dotnet add package Kanadeiar.Api
```

Регистрация в сервисах:
```sharp
builder.Services.KndAddHealthCheck();
```

Регистрация в конце конвейере приложения:

```sharp
app.MapHealthChecks("/healthz");
```