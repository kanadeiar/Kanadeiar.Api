namespace gRpc1ClientApi.Services;

public class ClientGrpcService : ClientInfo.ClientInfoBase
{
    private readonly ILogger<ClientGrpcService> _logger;
    public ClientGrpcService(ILogger<ClientGrpcService> logger)
    {
        _logger = logger;
    }

    public override Task<PagedResponse> GetPaged(PagedRequest request, ServerCallContext context)
    {
        var clients = Enumerable.Range(1, request.Count).Select(x => new ClientDto 
        {
            Id = x + request.Offset,
            UserId = 1,
            LastName = "dd11",
            FirstName = "aa22",
            Patronymic = "dd",
            BirthDay = Timestamp.FromDateTime(DateTime.UtcNow),
            RowVersion = ByteString.CopyFrom(new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
        });
        var response = new PagedResponse();
        foreach (var item in clients)
        {
            response.Clients.Add(item);
        }
        return Task.FromResult(response);
    }

    public override Task<CountRespose> GetCount(Empty request, ServerCallContext context)
    {
        var response = new CountRespose { Count = 100 };
        return Task.FromResult(response);
    }

    public override Task<ClientDto> GetById(IdRequest request, ServerCallContext context)
    {
        var client = new ClientDto { 
            Id = request.Id, 
            UserId = 1,
            LastName = "dd11", 
            FirstName = "aa22", 
            Patronymic = "dd", 
            BirthDay = Timestamp.FromDateTime(DateTime.UtcNow),
            RowVersion = ByteString.CopyFrom(new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
        };
        return Task.FromResult(client);
    }

    public override Task<AddedResponse> Add(ClientDto request, ServerCallContext context)
    {
        var added = new AddedResponse { Id = 101 };
        return Task.FromResult(added);
    }

    public override Task<SuccessResponse> Update(ClientDto request, ServerCallContext context)
    {
        var success = new SuccessResponse { Success = true };
        return Task.FromResult(success);
    }

    public override Task<SuccessResponse> Delete(IdRequest request, ServerCallContext context)
    {
        var success = new SuccessResponse { Success = true };
        return Task.FromResult(success);
    }
}
