namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса количества элементов
/// </summary>
public class GetClientCountQueryHanlder : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    public GetClientCountQueryHanlder(IClientRepository repository, IDbConnectionFactory connectionFactory)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Обработка запроса количества элементов
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Количество элементов</returns>
    public async Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var count = await db.ExecuteScalarAsync<int>(@"
SELECT COUNT(*) FROM Clients");
        return count;
        //var count = _repository.Query.Count();
        //return Task.FromResult(count);
    }
}
