namespace gRpc1ClientInfrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private IList<Client> _clients;

    public ClientRepository()
    {
        _clients = Enumerable.Range(1, 100).Select(x => new Client
        {
            Id = x,
            UserId = 1,
            LastName = $"Иванов_{x}",
            FirstName = $"Иван_{x}",
            Patronymic = $"Иванович_{x}",
            BirthDay = DateTime.Today.AddYears(-20),
            RowVersion = new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 },
        }).ToList();
    }

    public IQueryable<Client> Query => _clients.AsQueryable();

    public async IAsyncEnumerable<Client> GetPagedAsync(int offset, int count, CancellationToken cancellationToken)
    {
        foreach (var item in _clients
            .OrderBy(x => x.Id)
            .Skip(offset)
            .Take(count))
        {
            yield return item;
        }
    }

    public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_clients.FirstOrDefault(x => x.Id == id));
    }

    public Task<Client> AddAsync(Client entity, CancellationToken cancellationToken)
    {
        _clients.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Client entity, CancellationToken cancellationToken)
    {
        var item = _clients.FirstOrDefault(x => x.Id == entity.Id);
        item.UserId = entity.UserId;
        item.LastName = entity.LastName;
        item.FirstName = entity.FirstName;
        item.Patronymic = entity.Patronymic;
        item.BirthDay = entity.BirthDay;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Client entity)
    {
        var item = _clients.FirstOrDefault(x => x.Id == entity.Id);
        _clients.Remove(item);
        return Task.CompletedTask;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}
