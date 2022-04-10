## Swagger

Удобный интерфейс пользователя для работы с REST API сервисами.

Регистрация в сервисах:
```sharp
builder.Services.AddServiceSwagger("WebApiTest1");
```

Использование в конвейере:
```sharp
app.UseSwagger();
app.UseSwaggerUI();
```

> Обязательно зарегать их в сервисах для корректной работы: 

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();