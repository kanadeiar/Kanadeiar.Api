# Репозитории

[Назад](./Index.md)

> Только одно ключевое поле

Добавить пакет в Domain:
```sharp
dotnet add package Kanadeiar.Core
```
Добавить пакет в Application:
```sharp
dotnet add package Kanadeiar.Api
```

## Модификация сущностей в Domain

Добавить реализацию интерфейса IKndEntity в базовую сущность, в параметр-тип установить тип ключа
```sharp
abstract public class Entity : IKndEntity<int>
{
    [Key]
    public int Id { get; set; }
}
```

## Определение репозитория в Application

Определить интерфейс используемого в этом слое репозитория интерфейсом IKndRepositoryAsync, где в параметры-типы передавать для каждого репозитория тип сущности и тип ключевого поля
```sharp
public interface IClientRepository : IKndRepositoryAsync<Client, int>
{
}
```

Использовать репозиторий - запросы:
```sharp
_clientRepository.Query()
await _clientRepository.GetPagedAsync(offset, count, cancellationToken)
await _clientRepository.GetByIdAsync(request.Id)
```
команды:
```sharp
var result = await _clientRepository.AddAsync(entity);
await _clientRepository.UpdateAsync(entity);
await _clientRepository.DeleteAsync(entity);
var count = await _clientRepository.CommitAsync(CancellationToken cancellationToken);
```

## Реализация репозитория в Infrastructure

Реализация репозитория:
```sharp
public class ClientRepository : KndRepositoryAsync<Client, int>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }
}
```
Наследовать реализацию базовую репозитрия, пре необходимости - дополнить, указать в параметрах-типах сущность и тип ключевого поля

В конструкторе - передать контекст базы данных

## Регистрация репозиториев в сервисах приложения:

В слое Infrastructure
```sharp
public static IServiceCollection MyAddRepositories(this IServiceCollection services)
{
    services.AddScoped<IClientRepository, ClientRepository>();

    return services;
}
```

В сервисах приложения:

```sharp
builder.Services.MyAddRepositories();
```
