namespace gRpc1ClientApplication.Contracts.Queries;

public class GetClientByIdQuery : IRequest<Client?>
{
    public int Id { get; }

    public GetClientByIdQuery(int id)
    {
        Id = id;
    }
}
