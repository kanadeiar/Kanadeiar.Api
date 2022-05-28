namespace Lab1ClientApplication.Implementations.Queries;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, GetClientByIdQueryResult>
{
    private readonly IClientRepository _repository;
    public GetClientByIdQueryHandler(IClientRepository repository) 
        => _repository = repository;

    public async Task<GetClientByIdQueryResult> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        if (await _repository.GetByIdAsync(request.Id, cancellationToken) is Client item)
        {
            return new GetClientByIdQueryResult(item);
        }
        return null;
    }
}
