namespace Lab1ClientApplication.Implementations.Queries;

public class GetClientCountQueryHandler : IRequestHandler<GetClientCountQuery, int>
{
    private readonly IClientRepository _repository;
    public GetClientCountQueryHandler(IClientRepository repository)
        => _repository = repository;

    public async Task<int> Handle(GetClientCountQuery request, CancellationToken cancellationToken)
    {
        var count = _repository.Query.Count();
        return count;
    }
}
