namespace gRpc1ClientApplication.Implementations.Queries;

public class GetPagedClientQueryHandler : IStreamRequestHandler<GetPagedClientQuery, Client>
{
    private readonly IClientRepository _repository;

    public GetPagedClientQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async IAsyncEnumerable<Client> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        var offset = request.Offset;
        var count = request.Count;
        await foreach (var item in _repository.GetPagedAsync(offset, count, cancellationToken))
        {
            yield return item;
        }
    }
}
