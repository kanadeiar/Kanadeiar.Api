namespace Rest1ClientApplication.Queries;

/// <summary>
/// Обработчик запроса одного элемента
/// </summary>
public class GetClientByIdHandler : IRequestHandler<GetClientById, ClientDto?>
{
    private readonly DbContext _context;

    public GetClientByIdHandler(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Запрос одной записи
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ClientDto?> Handle(GetClientById request, CancellationToken cancellationToken)
    {
        if (await _context.Set<Client>().FindAsync(new object[] { request.Id }, cancellationToken) is Client item)
        {
            return item.Adapt<ClientDto>();
        }
        return null;
    }
}
