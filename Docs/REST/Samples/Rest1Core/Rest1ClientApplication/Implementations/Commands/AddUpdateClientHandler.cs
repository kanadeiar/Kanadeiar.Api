namespace Rest1ClientApplication.Implementations.Commands;

/// <summary>
/// Обработчик команда добавления и обновления элемента
/// </summary>
public class AddUpdateClientHandler : IRequestHandler<AddUpdateClient, int>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<AddUpdateClientHandler> _logger;
    /// <summary> </summary>
    public AddUpdateClientHandler(IClientRepository repository, ILogger<AddUpdateClientHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    /// <summary>
    /// Команда добавления или обновления элемента
    /// </summary>
    /// <param name="request">запрос</param>
    /// <param name="cancellationToken">токен отмены</param>
    /// <returns>Идентификатор элемента</returns>
    public async Task<int> Handle(AddUpdateClient request, CancellationToken cancellationToken)
    {
        var config = new TypeAdapterConfig().ForType<ClientDto, Client>().Ignore(x => x.Id).Config;
        var item = request.Model.Adapt<Client>(config);
        if (request.Id == 0)
        {
            await _repository.AddAsync(item, cancellationToken);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", item.Id);
            return item.Id;
        }
        else if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            item.Adapt(entity, config);
            entity.Id = request.Id;
            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Обновление элемента - клиента с идентификатором id: {0}", entity.Id);
            return entity.Id;
        }
        _logger.LogError("Не удалось добавить или обновить сущность с идентификатором id: {0}", request.Id);
        return 0;
    }
}
