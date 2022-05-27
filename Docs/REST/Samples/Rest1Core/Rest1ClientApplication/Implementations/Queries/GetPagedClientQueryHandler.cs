namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса множества элементов
/// </summary>
public class GetPagedClientQueryHandler : IStreamRequestHandler<GetPagedClientQuery, ClientDto>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    public GetPagedClientQueryHandler(IClientRepository repository, IDbConnectionFactory connectionFactory)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Запрос множества элементов
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Асинхронная коллекция</returns>
    public async IAsyncEnumerable<ClientDto> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
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
