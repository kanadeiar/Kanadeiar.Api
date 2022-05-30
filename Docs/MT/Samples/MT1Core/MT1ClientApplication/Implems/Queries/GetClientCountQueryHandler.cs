namespace MT1ClientApplication.Implems.Queries;

/// <summary>
/// Обработчик запроса количества элементов
/// </summary>
public class GetClientCountQueryHandler : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepo _repo;
    public GetClientCountQueryHandler(IClientRepo repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Обработка запроса количества элементов
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество</returns>
    public async Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        var count = _repo.Query.Count();
        return count;
    }
}
