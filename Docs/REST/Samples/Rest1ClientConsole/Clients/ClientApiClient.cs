using Rest1ClientConsoleApp.Interfaces;

namespace Rest1ClientConsoleApp.Clients;

/// <summary>
/// Апи клиент данных клиентов
/// </summary>
public class ClientApiClient : KndHttpJsonClient, IClientApiClient
{
    public ClientApiClient(HttpClient client) : base(client, "client")
    {
    }

    /// <summary>
    /// Получение элементов со смещением и количеством
    /// </summary>
    /// <param name="offset">Смещение</param>
    /// <param name="count">Количество</param>
    /// <returns>Элементы</returns>
    public async Task<IEnumerable<Client>> GetPagedAsync(int offset, int count)
    {
        var dtos = await GetAsync<IEnumerable<ClientDto>>($"{Address}?offset={offset}&count={count}");
        if (dtos != null)
        {
            return dtos.Adapt<IEnumerable<Client>>();
        }
        return Enumerable.Empty<Client>();
    }

    /// <summary>
    /// Количество элементов
    /// </summary>
    /// <returns>Количество</returns>
    public async Task<int> GetCountAsync()
    {
        var count = await GetAsync<int>($"{Address}/count");
        return count;
    }

    /// <summary>
    /// Один элемент
    /// </summary>
    /// <param name="id">Индекс</param>
    /// <returns>Элемент</returns>
    public async Task<Client?> GetByIdAsync(int id)
    {
        var dto = await GetAsync<ClientDto>($"{Address}/{id}");
        if (dto != null)
        {
            return dto.Adapt<Client>();
        }
        return default;
    }

    /// <summary>
    /// Добавить один элемент
    /// </summary>
    /// <param name="client">Элемент новый</param>
    /// <returns>Идентификатор присвоенный</returns>
    public async Task<int> AddAsync(Client client)
    {
        var dto = client.Adapt<ClientDto>();
        var id = await PostAsync($"{Address}", dto);
        return id;
    }

    /// <summary>
    /// Обновить один элемент
    /// </summary>
    /// <param name="client">Новые данные + идентификатор обновляемого элемента</param>
    /// <returns>Успешность обновления</returns>
    /// <exception cref="ArgumentException">Не указан идентификатор элемента</exception>
    public async Task<bool> UpdateAsync(Client client)
    {
        if (client.Id == 0)
            throw new ArgumentException("Не указан идентификатор элемента", nameof(client.Id));
        var dto = client.Adapt<ClientDto>();
        var success = await PutAsync($"{Address}/{client.Id}", dto);
        return success;
    }

    /// <summary>
    /// Удалить один элемент
    /// </summary>
    /// <param name="id">Идентификатор удаляемого элемента</param>
    /// <returns>Успешность удаления</returns>
    /// <exception cref="ArgumentException">Не указан идентификатор элемента</exception>
    public async Task<bool> DeleteAsync(int id)
    {
        if (id == 0)
            throw new ArgumentException("Не указан идентификатор элемента", nameof(id));
        var success = await DeleteAsync($"{Address}/{id}");
        return success;
    }
}
