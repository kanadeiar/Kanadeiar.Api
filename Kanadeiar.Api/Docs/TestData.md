# Начальные тестовые данные

[Назад](./../README.md)

В приложении реализовать класс заполнения тестовыми данными:

```sharp
public class TestData : IKndTestData
{
...

    public async Task SeedTestData(IServiceProvider provider)
    {
...
    }
}
```

Зарегистрировать в сервисах приложения:

```sharp
builder.Services.AddTransient<TestData>();
```

В конце конвейера приложения установить заполнение базы данных тестовыми данными:

```sharp
app.KndSeedTestData<TestData>();
```