namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdHandler : IRequestHandler<GetClientById, ClientDto?>
{
    private readonly IClientRepository _repository;

    public GetClientByIdHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Запрос одного элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элемент</returns>
    public async Task<ClientDto?> Handle(GetClientById request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is Client item)
        {
            return item.Adapt<ClientDto>();
        }
        return null;
    }
}
