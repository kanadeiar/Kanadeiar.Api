namespace MT1ConsoleApp.Interfaces;

/// <summary>
/// HTTP REST клиент
/// </summary>
public interface IClientApiClient
{
    Task<IEnumerable<Client>> GetPagedAsync(int offset, int count);
    Task<int> GetCountAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<int> AddAsync(Client client);
    Task<bool> UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
}
