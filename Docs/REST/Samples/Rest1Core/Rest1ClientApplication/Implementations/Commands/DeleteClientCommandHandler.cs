namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик удаления элемента
/// </summary>
public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, bool>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<AddUpdateClientCommandHandler> _logger;

    /// <summary> </summary>
    public DeleteClientCommandHandler(IClientRepository repository, ILogger<AddUpdateClientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Обработка удаления элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность удаления</returns>
    public async Task<bool> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            await _repository.DeleteAsync(entity);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Удаление элемента - клиента с идентификатором id: {0}", request.Id);
            return true;
        }
        _logger.LogError("Не удалось удалить элемент - клиент с идентификатором Id: {0}", request.Id);
        return false;
    }
}
