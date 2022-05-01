# HTTP Json Клиент

[Назад](./../README.md)

Установить пакет:
```sharp
dotnet add package Kanadeiar.Api
```

Определить необходимые методы клиента, пример:
```sharp
public interface IClientApiClient
{
    Task<IEnumerable<Client>> GetPagedAsync(int offset, int count);
    Task<int> GetCountAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<int> AddAsync(Client client);
    Task<bool> UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
}
```

Реализовать клиент.

Пример реализации клиента:
```sharp
public class ClientApiClient : KndHttpJsonClient, IClientApiClient
{
    public ClientApiClient(HttpClient client) : base(client, "client")
    {
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        var dto = await GetAsync<ClientDto>($"{Address}/{id}");
        if (dto != null)
        {
            return dto.Adapt<Client>();
        }
        return default;
    }
}
```

Использовать клиент:
```sharp
if (await client.GetByIdAsync(1) is { } data)
{
    Console.WriteLine("Один элемент: {0} {1} {2} {3} {4}", data.Id, data.LastName, data.FirstName, data.Patronymic, data.BirthDay.ToString("dd.MM.yyyy"));
}
else
{
    Console.WriteLine("Элемент не найден");
}
```
