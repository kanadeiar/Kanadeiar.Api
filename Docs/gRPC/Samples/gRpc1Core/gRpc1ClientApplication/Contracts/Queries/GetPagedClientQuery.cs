namespace gRpc1ClientApplication.Contracts.Queries;

public class GetPagedClientQuery : IStreamRequest<Client>
{
    public int Offset { get; }

    public int Count { get; }

    public GetPagedClientQuery(int offset, int count)
    {
        Offset = offset;
        Count = count;
    }
}
