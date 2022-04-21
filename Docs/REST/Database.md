# База данных EF

[Назад](./Index.md)

Образец определения базы данных:
```sharp
public class ClientContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    { }
}
```

Строка подключения в файле конфигурации приложения:
```xml
"ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=ClientService;MultipleActiveResultSets=True;Integrated Security=true"
```

Образец регистрации в сервисах приложения:
```sharp
services.AddDbContext<ClientContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionString"),
        o => o.MigrationsAssembly("Rest1ClientInfrastructure"));
#if DEBUG
    options.EnableSensitiveDataLogging(); // писать в логи все сведения
#endif
});

services.AddScoped<DbContext, ClientContext>(); //если одна база данных в приложении
```

## Команды создания миграций

Образец команды:
```cmd
dotnet ef migrations add init
```

Если добавлять в отдельную библиотеку от проекта приложения, то образец команды c указанием контекста:

```cmd
dotnet ef --startup-project ../../Rest1ClientApi/ migrations add init --context ClientContext
```
