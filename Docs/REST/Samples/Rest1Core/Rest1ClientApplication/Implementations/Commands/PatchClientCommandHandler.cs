namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик изменения элемента
/// </summary>
public class PatchClientCommandHandler : IRequestHandler<PatchClientCommand, bool>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<PatchClientCommandHandler> _logger;
    /// <summary> </summary>
    public PatchClientCommandHandler(IClientRepository repository, ILogger<PatchClientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Обработка изменения элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Успешность удаления</returns>
    public async Task<bool> Handle(PatchClientCommand request, CancellationToken cancellationToken)
    {
        if (!request.Patch.Operations.Any())
            throw new ArgumentNullException(nameof(request.Patch.Operations));
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (item is { })
        {
            var patch = request.Patch;
            patch.ApplyTo(item);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Изменение клиента с идентификатором Id: {0}", item.Id);
            return true;
        }
        _logger.LogError("Не удалось изменить клиента с идентификатором id: {0}", item?.Id);
        return false;
    }
}
