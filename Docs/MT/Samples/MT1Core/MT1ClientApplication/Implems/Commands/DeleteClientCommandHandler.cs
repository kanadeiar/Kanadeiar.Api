namespace MT1ClientApplication.Implems.Commands;

/// <summary>
/// Обработчик удаления элемента
/// </summary>
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IClientRepo _repo;
    private readonly ILogger<DeleteClientCommandHandler> _logger;
    public DeleteClientCommandHandler(IClientRepo repo, ILogger<DeleteClientCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    /// <summary>
    /// Обработка удаления элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Успех удаления</returns>
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        if (await _repo.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            await _repo.DeleteAsync(entity);
            await _repo.CommitAsync(cancellationToken);
            _logger.LogInformation("Удален элемент клиент с идентификатором id: {0}", request.Id);
            return true;
        }
        _logger.LogError("Не удалось удалить элемент - клиент с идентификатором Id: {0}", request.Id);
        return false;
    }
}
