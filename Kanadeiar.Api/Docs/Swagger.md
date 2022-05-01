# Swagger

[Назад](./../README.md)

Установить пакет:
```sharp
dotnet add package Kanadeiar.Api
```

Регистрация в сервисах:
```sharp
builder.Services.KndAddSwagger("WebApiTest1");
builder.Services.KndAddSwagger("WebApiTest1", filename: "info.xml");
builder.Services.KndAddSwagger("WebApiTest1", filename: "info.xml", domainFilenames: new[] { "sample.application.xml" });

```

Зарегистрировать использование в конвейере, желательно в самом начале, и в среде разработки приложения:
```sharp
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts(); //если нужно
}
```

> Обязательно зарегать в сервисах для корректной работы Swagger: 

```sharp
builder.Services.AddEndpointsApiExplorer();
```