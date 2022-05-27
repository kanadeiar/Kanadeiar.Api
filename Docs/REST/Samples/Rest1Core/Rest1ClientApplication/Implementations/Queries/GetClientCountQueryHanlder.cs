namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса количества элементов
/// </summary>
public class GetClientCountQueryHanlder : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepository _repository;
    private readonly IConfiguration _configuration;
    public GetClientCountQueryHanlder(IClientRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    /// <summary>
    /// Обработка запроса количества элементов
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Количество элементов</returns>
    public async Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetValue<string>("ConnectionString");
        using var db = new SqlConnection(connectionString);
        var count = await db.ExecuteScalarAsync<int>(@"
SELECT COUNT(*) FROM Clients");
        return count;
        //var count = _repository.Query.Count();
        //return Task.FromResult(count);
    }
}
