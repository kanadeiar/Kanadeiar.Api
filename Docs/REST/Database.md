# База данных на ORM Entity Framework

[Назад](./Index.md)

Консоль:
```sharp
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```

Добавить пакеты в приложение:
```sharp
dotnet add package Microsoft.EntityFrameworkCore.Design
```
Добавить пакет в инфраструктурную библиотеку:
```sharp
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

Образец определения базы данных:
```sharp
public class ClientContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    { }
}
```

Строка подключения в файле конфигурации приложения appsettings.json:
```xml
"ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=ClientService;MultipleActiveResultSets=True;Integrated Security=true"
```

Образец регистрации в сервисах приложения:
```sharp
services.AddDbContext<ClientContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionString"),
        o => o.MigrationsAssembly("RabbitMq1ClientInfrastructure")); 
#if DEBUG
    options.EnableSensitiveDataLogging(); // писать в логи все сведения
#endif
});

services.AddScoped<DbContext, ClientContext>(); //допустимо только если одна база данных в приложении
```

> Если миграции находятся в той-же папке, что и определение базы данных, то можно не уточнять местонахождение миграций - MigrationsAssembly()

## Команды создания миграций

Образец команды:
```cmd
dotnet ef migrations add init
```

Если добавлять в отдельную библиотеку от проекта приложения, то образец команды c указанием контекста:

```cmd
dotnet ef --startup-project ../../Rest1ClientApi/ migrations add init --context ClientContext
```


