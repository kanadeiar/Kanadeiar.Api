namespace MT1ClientApplication.Implems.Queries;

/// <summary>
/// Обработчик запроса элементов с постраничной разбивкой
/// </summary>
public class GetPagedClientQueryHanlder : IRequestHandler<GetPagedClientQuery, IEnumerable<Client>>
{
    private readonly IClientRepo _repo;
    public GetPagedClientQueryHanlder(IClientRepo repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Обработка запроса с постраничной разбивкой
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элементы</returns>
    public async Task<IEnumerable<Client>> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        var elements = await _repo.GetPaged(request.Offset, request.Count, cancellationToken);
        return elements;
    }
}
