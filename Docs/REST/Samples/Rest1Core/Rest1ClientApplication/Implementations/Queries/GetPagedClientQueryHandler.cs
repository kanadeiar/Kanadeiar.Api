namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса множества элементов
/// </summary>
public class GetPagedClientQueryHandler : IStreamRequestHandler<GetPagedClientQuery, ClientDto>
{
    private readonly IClientRepository _repository;
    private readonly IConfiguration _configuration;
    public GetPagedClientQueryHandler(IClientRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    /// <summary>
    /// Запрос множества элементов
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Асинхронная коллекция</returns>
    public async IAsyncEnumerable<ClientDto> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetValue<string>("ConnectionString");
        using var db = new SqlConnection(connectionString);
        var items = (await db.QueryAsync<Client>(@"
SELECT * FROM Clients
ORDER BY Id
OFFSET @offset ROWS FETCH NEXT @count ROWS ONLY",
        new { offset = request.Offset, count = request.Count }));
        foreach (var item in items)
        {
            yield return item.Adapt<ClientDto>();
        }
        //await foreach (var item in _repository.GetPagedAsync(request.Offset, request.Count, cancellationToken))
        //{
        //    yield return item.Adapt<ClientDto>();
        //}
    }
}
