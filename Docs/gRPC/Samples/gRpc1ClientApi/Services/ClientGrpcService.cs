namespace gRpc1ClientApi.Services;

public class ClientGrpcService : ClientInfo.ClientInfoBase
{
    private readonly ILogger<ClientGrpcService> _logger;
    public ClientGrpcService(ILogger<ClientGrpcService> logger)
    {
        _logger = logger;
    }

    public override Task<ClientDto> GetById(IdRequest request, ServerCallContext context)
    {
        var client = new ClientDto { 
            Id = 1, 
            UserId = 1,
            LastName = "dd11", 
            FirstName = "aa22", 
            Patronymic = "dd", 
            BirthDay = Timestamp.FromDateTime(DateTime.UtcNow),
            RowVersion = ByteString.CopyFrom(new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 }),
        };
        return Task.FromResult(client);
    }
}
