namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса множества элементов
/// </summary>
public class GetPagedClientQueryHandler : IStreamRequestHandler<GetPagedClientQuery, ClientDto>
{
    private readonly IClientRepository _repository;

    public GetPagedClientQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Запрос множества элементов
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Асинхронная коллекция</returns>
    public async IAsyncEnumerable<ClientDto> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        var offset = request.Offset;
        var count = request.Count;
        await foreach (var item in _repository.GetPagedAsync(offset, count, cancellationToken))
        {
            yield return item.Adapt<ClientDto>();
        }
    }
}
