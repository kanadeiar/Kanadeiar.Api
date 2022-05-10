namespace gRpc1ClientApplication.Implementations.Queries;

public class GetClientCountQueryHandler : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepository _repository;

    public GetClientCountQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        var count = _repository.Query.Count();
        return Task.FromResult(count);
    }
}
