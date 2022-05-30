namespace MT1ClientApplication.Implems.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client?>
{
    private readonly IClientRepo _repo;
    public GetClientByIdQueryHandler(IClientRepo repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Обработка запроса одного элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Элемент</returns>
    public async Task<Client?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repo.GetByIdAsync(request.Id, cancellationToken) is Client item)
        {
            return item;
        }
        return null;
    }
}
