namespace Lab1ClientApplication.Contracts.Queries;

public class GetClientCountQuery : IRequest<int>
{
    public GetClientCountQuery()
    { }

    public interface IOk
    {
        int Count { get; set; }
    }
}
