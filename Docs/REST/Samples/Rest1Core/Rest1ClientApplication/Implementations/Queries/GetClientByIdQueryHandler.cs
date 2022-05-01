namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto?>
{
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Запрос одного элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элемент</returns>
    public async Task<ClientDto?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is Client item)
        {
            return item.Adapt<ClientDto>();
        }
        return null;
    }
}
