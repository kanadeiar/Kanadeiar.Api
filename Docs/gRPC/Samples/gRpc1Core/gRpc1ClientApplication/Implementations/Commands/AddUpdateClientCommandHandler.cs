namespace gRpc1ClientApplication.Implementations.Commands;

public class AddUpdateClientCommandHandler : IRequestHandler<AddUpdateClientCommand, int>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<AddUpdateClientCommandHandler> _logger;

    public AddUpdateClientCommandHandler(IClientRepository repository, ILogger<AddUpdateClientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> Handle(AddUpdateClientCommand request, CancellationToken cancellationToken)
    {
        var item = request.Client;
        if (request.Id == 0)
        {
            await _repository.AddAsync(item, cancellationToken);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", item.Id);
            return item.Id;
        }
        else if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            item.Adapt(entity);
            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Обновление элемента - клиента с идентификатором id: {0}", entity.Id);
            return entity.Id;
        }
        _logger.LogError("Не удалось добавить или обновить сущность с идентификатором id: {0}", request.Id);
        return 0;
    }
}
