namespace Lab1ClientApplication.Contracts.Queries;

public class GetClientByIdQuery : IRequest<GetClientByIdQueryResult>
{
    public int Id { get; }

    public GetClientByIdQuery(int id)
    {
        Id = id;
    }
}
