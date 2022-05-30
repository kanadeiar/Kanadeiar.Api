namespace MT1ClientApplication.Implems.Commands;

/// <summary>
/// Обработчик обновления элемента
/// </summary>
public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
{
    private readonly IClientRepo _repo;
    private readonly ILogger<UpdateClientCommandHandler> _logger;
    public UpdateClientCommandHandler(IClientRepo repo, ILogger<UpdateClientCommandHandler> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    /// <summary>
    /// Обработка обновления элемента
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Успешность обновления</returns>
    public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var config = new TypeAdapterConfig().ForType<Client, Client>().Ignore(x => x.Id).Config;
        if (await _repo.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            request.Client.Adapt(entity, config);
            entity.Id = request.Id;
            await _repo.UpdateAsync(entity, cancellationToken);
            await _repo.CommitAsync(cancellationToken);
            _logger.LogInformation("Обновлен элемент клиент с идентификатором id: {0}", entity.Id);
            return entity.Id == request.Id;
        }
        _logger.LogError("Не удалось добавить или обновить элемент клиент с идентификатором id: {0}", request.Id);
        return false;
    }
}
