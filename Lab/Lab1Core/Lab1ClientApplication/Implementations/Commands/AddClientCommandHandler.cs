namespace Lab1ClientApplication.Implementations.Commands;

public class AddClientCommandHandler : IRequestHandler<AddClientCommand, int>
{
    private readonly IClientRepository _repository;
    private readonly ILogger<AddClientCommandHandler> _logger;
    public AddClientCommandHandler(IClientRepository repository, ILogger<AddClientCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<int> Handle(AddClientCommand request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request.Client, cancellationToken);
        await _repository.CommitAsync(cancellationToken);
        _logger.LogInformation("Добавление нового элемента - клиента с новым идентификатором id: {0}", request.Client.Id);
        return request.Client.Id;
    }
}
