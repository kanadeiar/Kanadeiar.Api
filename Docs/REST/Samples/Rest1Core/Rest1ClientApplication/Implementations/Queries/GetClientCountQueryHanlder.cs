namespace Rest1ClientApplication.Implementations.Queries;

/// <summary>
/// Обработчик запроса количества элементов
/// </summary>
public class GetClientCountQueryHanlder : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepository _repository;

    public GetClientCountQueryHanlder(IClientRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Обработка запроса количества элементов
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Количество элементов</returns>
    public Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        var count = _repository.Query.Count();
        return Task.FromResult(count);
    }
}
