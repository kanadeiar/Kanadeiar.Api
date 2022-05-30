namespace MT1ClientApplication.Implems.Commands;

/// <summary>
/// Обработчик добавления нового элемента
/// </summary>
public class AddClientCommandHandler : IRequestHandler<AddClientCommand, int>
{
    private readonly IClientRepo _repo;
    private readonly ILogger<AddClientCommandHandler> _logger;
    public AddClientCommandHandler(IClientRepo repo, ILogger<AddClientCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    /// <summary>
    /// Добавление нового элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Новый идентификатор</returns>
    public async Task<int> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        await _repo.AddAsync(request.Client, cancellationToken);
        await _repo.CommitAsync(cancellationToken);
        _logger.LogInformation("Добавлен новый элемент клиент с идентификатором id: {0}", request.Client.Id);
        return request.Client.Id;
    }
}
