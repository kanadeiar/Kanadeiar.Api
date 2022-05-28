namespace Rest1ClientInfrastructure.Repositories;

/// <summary>
/// Реализация репозитория клиентов на основе ORM Dapper
/// </summary>
public class ClientDapperRepository : IClientRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<ClientDapperRepository> _logger;
    public ClientDapperRepository(IDbConnectionFactory connectionFactory, ILogger<ClientDapperRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public IQueryable<Client> Query
    {
        get 
        {
            using var db = _connectionFactory.CreateConnection();
            var items = db.Query<Client>(@"
SELECT * FROM Clients"
);
            return items.AsQueryable();
        }
    }

    public async IAsyncEnumerable<Client> GetPagedAsync(int offset, int count, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var items = await db.QueryAsync<Client>(@"
SELECT * FROM Clients
ORDER BY Id
OFFSET @offset ROWS FETCH NEXT @count ROWS ONLY",
        new { offset = offset, count = count });
        foreach (var item in items)
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;
            yield return item;
        }
    }

    public async Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var item = (await db.QueryAsync<Client>(@"
SELECT * FROM Clients 
WHERE Id = @id",
        new { id })).FirstOrDefault();
        return item;
    }

    public async Task<Client> AddAsync(Client entity, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var sqlQuery = @"
INSERT INTO Clients (UserId, LastName, FirstName, Patronymic, BirthDay) 
VALUES (@UserId, @LastName, @FirstName, @Patronymic, @BirthDay);
SELECT CAST(SCOPE_IDENTITY() as int)";
        int? itemId = await db.QueryFirstAsync<int>(sqlQuery, entity);
        entity.Id = itemId.Value;
        _logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", itemId.Value);
        return entity;
    }

    public async Task UpdateAsync(Client entity, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var sqlQuery = @"
UPDATE Clients 
SET UserId = @UserId, LastName = @LastName, FirstName = @FirstName, Patronymic = @Patronymic, BirthDay = @BirthDay
WHERE Id = @Id";
        var affectedrows = await db.ExecuteAsync(sqlQuery, entity);
        _logger.LogInformation("Обновление элемента - клиента с идентификатором id: {0}", entity.Id);
    }

    public async Task DeleteAsync(Client entity)
    {
        using var db = _connectionFactory.CreateConnection();
        var sqlQuery = @"
DELETE FROM Clients 
WHERE Id = @id";
        var affectedrows = await db.ExecuteAsync(sqlQuery, new { id = entity.Id });
        _logger.LogInformation("Удаление элемента - клиента с идентификатором id: {0}", entity.Id);
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}
