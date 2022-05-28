namespace Lab1ClientApplication.Implementations.Commands;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, bool>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<UpdateClientCommandHandler> _logger;
    public UpdateClientCommandHandler(IClientRepository repository, ILogger<UpdateClientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<bool> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var config = new TypeAdapterConfig().ForType<Client, Client>().Ignore(x => x.Id).Config;
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is { } entity)
        {
            request.Client.Adapt(entity, config);
            entity.Id = request.Id;
            await _repository.UpdateAsync(entity, cancellationToken);
            await _repository.CommitAsync(cancellationToken);
            _logger.LogInformation("Обновление элемента - клиента с идентификатором id: {0}", entity.Id);
            return entity.Id == request.Id;
        }
        _logger.LogError("Не удалось добавить или обновить сущность с идентификатором id: {0}", request.Id);
        return false;
    }
}
