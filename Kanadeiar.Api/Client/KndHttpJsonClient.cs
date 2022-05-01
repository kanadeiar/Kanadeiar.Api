namespace Kanadeiar.Api.Client;

using System.Net.Http.Json;

/// <summary>
/// Базовый клиент
/// </summary>
public abstract class KndHttpJsonClient : IDisposable
{
    protected readonly HttpClient Client;
    protected readonly string Address;

    public KndHttpJsonClient(HttpClient client, string address)
    {
        Client = client;
        Address = address;
    }

    /// <summary> 
    /// Получение данных с веб апи сервиса 
    /// </summary>
    /// <typeparam name="T?">тип данных</typeparam>
    /// <param name="url">конечная точка</param>
    /// <param name="cancel">токен отмены</param>
    /// <returns>данные</returns>
    protected async Task<T?> GetAsync<T>(string url, CancellationToken cancel = default)
    {
        var response = await Client
            .GetAsync(url, cancel).ConfigureAwait(false);
        return await response.EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<T>(cancellationToken: cancel);
    }

    /// <summary> 
    /// Добавление данных в веб апи сервис 
    /// </summary>
    /// <typeparam name="T">тип данных</typeparam>
    /// <param name="url">конечная точка</param>
    /// <param name="item">данные</param>
    /// <param name="cancel">токен отмены</param>
    /// <returns>статус добавления</returns>
    protected async Task<int> PostAsync<T>(string url, T item, CancellationToken cancel = default)
    {
        var response = await Client
            .PostAsJsonAsync(url, item, cancel).ConfigureAwait(false);
        return await response.EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<int>(cancellationToken: cancel);
    }

    /// <summary> 
    /// Обновление данных в веб апи сервисе 
    /// <param>
    /// <typeparam name="T">тип данных</typeparam>
    /// <param name="url">конечная точка</param>
    /// <param name="item">данные</param>
    /// <param name="cancel">токен отмены</param>
    /// <returns>результат обновления</returns>
    protected async Task<bool> PutAsync<T>(string url, T item, CancellationToken cancel = default)
    {
        var response = await Client
            .PutAsJsonAsync(url, item, cancel).ConfigureAwait(false);
        return await response.EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel);
    }

    /// <summary> 
    /// Удаление данных из веб апи сервиса 
    /// </summary>
    /// <param name="url">конечная точка</param>
    /// <param name="cancel">токен отмены</param>
    /// <returns>результат обновления</returns>
    protected async Task<bool> DeleteAsync(string url, CancellationToken cancel = default)
    {
        var response = await Client
            .DeleteAsync(url, cancel).ConfigureAwait(false);
        return await response.EnsureSuccessStatusCode()
            .Content.ReadFromJsonAsync<bool>(cancellationToken: cancel);
    }

    #region Утилизация ресурсов
    public void Dispose() => Dispose(true);

    private bool _disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        _disposed = true;
        if (disposing)
        {
            // освободить ресурсы 
        }
    }
    #endregion
}
