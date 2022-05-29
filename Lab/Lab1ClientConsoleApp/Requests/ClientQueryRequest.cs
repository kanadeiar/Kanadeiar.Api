namespace Lab1ClientConsoleApp.Requests;

public class ClientQueryRequest
{
    IRequestClient<GetClientCountQuery> _clientCountQuery;
    IRequestClient<GetPagedClientQuery> _pagedClientQuery;
    IRequestClient<GetClientByIdQuery> _clientByIdQuery;
    public ClientQueryRequest(IBus bus)
    {
        _clientCountQuery = bus.CreateRequestClient<GetClientCountQuery>();
        _pagedClientQuery = bus.CreateRequestClient<GetPagedClientQuery>();
        _clientByIdQuery = bus.CreateRequestClient<GetClientByIdQuery>();
    }

    public async Task<int> GetClientCountQuery()
    {
        var count = await _clientCountQuery.GetResponse<GetClientCountQuery.IOk>(new GetClientCountQuery());
        return count.Message.Count;
    }

    public async Task<IEnumerable<Client>> GetPagedClientQuery(int offset, int count)
    {
        var datas = await _pagedClientQuery.GetResponse<GetPagedClientQuery.IOk>(new GetPagedClientQuery(offset, count));
        return datas.Message.Clients;
    }

    public async Task<Client?> GetClientByIdQuery(int id)
    {
        var (okGet, _) = await _clientByIdQuery.GetResponse<GetClientByIdQuery.IOk, GetClientByIdQuery.INotFound>(new GetClientByIdQuery(1));
        if (okGet.IsCompletedSuccessfully)
        {
            var item = (await okGet).Message.Client;
            return item;
        }
        return null;
    }
}
