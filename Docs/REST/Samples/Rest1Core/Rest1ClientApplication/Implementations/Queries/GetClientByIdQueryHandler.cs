namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IClientRepository _repository;
    private readonly IConfiguration _configuration;
    public GetClientByIdQueryHandler(IClientRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    /// <summary>
    /// Запрос одного элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элемент</returns>
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var connectionString = _configuration.GetValue<string>("ConnectionString");
        using var db = new SqlConnection(connectionString);
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
