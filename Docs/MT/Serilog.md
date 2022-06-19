# Лог Serilog

[Назад](./Index.md)


Добавить пакеты в приложение:
```sharp
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Extensions.Logging
dotnet add package Serilog.Extensions.Logging.File
```

Зарегистрировать сервисы в приложении для записи в консоль и в файл:
```sharp
builder.Host.UseSerilog((host, log) =>
{
    log.ReadFrom.Configuration(host.Configuration)
        .MinimumLevel.Debug()
#if DEBUG
        .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
#else
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
#endif
        .WriteTo.RollingFile($@".\Logs\SampleApi_[{DateTime.Now:yyyy-MM-dd}].log")
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}");
});
```

