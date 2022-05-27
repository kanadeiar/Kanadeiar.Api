namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IClientRepository _repository;
    private readonly IDbConnectionFactory _connectionFactory;
    public GetClientByIdQueryHandler(IClientRepository repository, IDbConnectionFactory connectionFactory)
    {
        _repository = repository;
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Запрос одного элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элемент</returns>
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        using var db = _connectionFactory.CreateConnection();
        var item = (await db.QueryAsync<Client>(@"
SELECT * FROM Clients 
WHERE Id = @id",
        new { request.Id })).FirstOrDefault();
        if (item is { })
        {
            return item.Adapt<ClientDto>();
        }
        return null;
        //if (await _repository.GetByIdAsync(request.Id, cancellationToken) is Client item)
        //{
        //    return item.Adapt<ClientDto>();
        //}
        //return null;
    }
}
