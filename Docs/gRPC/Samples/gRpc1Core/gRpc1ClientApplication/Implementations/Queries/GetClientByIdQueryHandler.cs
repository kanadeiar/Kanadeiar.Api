namespace gRpc1ClientApplication.Implementations.Queries;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client?>
{
    private readonly IClientRepository _repository;

    public GetClientByIdQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Client?> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is Client item)
        {
            return item;
        }
        return null;
    }
}
