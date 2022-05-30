namespace Lab1ClientApplication.Implementations.Queries;

public class GetPagedClientQueryHandler : IStreamRequestHandler<GetPagedClientQuery, Client>
{
    private readonly IClientRepository _repository;
    public GetPagedClientQueryHandler(IClientRepository repository)
    {
        _repository = repository;
    }

    public async IAsyncEnumerable<Client> Handle(GetPagedClientQuery request, CancellationToken cancellationToken)
    {
        await foreach (var item in _repository.GetPagedAsync(request.Offset, request.Count, cancellationToken))
        {
            yield return item;
        }
    }
}
