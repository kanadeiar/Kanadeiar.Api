using gRpc1ClientApplication.Contracts.Commands;

namespace gRpc1ClientApi.Services;

public class ClientGrpcService : ClientInfo.ClientInfoBase
{
    private readonly IMediator _mediator;

    public ClientGrpcService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<PagedResponse> GetPaged(PagedRequest request, ServerCallContext context)
    {
        var items = await _mediator.CreateStream(new GetPagedClientQuery(request.Offset, request.Count), new CancellationToken()).ToArrayAsync();
        var response = new PagedResponse();
        var config = new TypeAdapterConfig().ForType<Client, ClientDto>()
            .Map(d => d.BirthDay, s => Timestamp.FromDateTime(s.BirthDay.ToUniversalTime()))
            .Map(d => d.RowVersion, s => ByteString.CopyFrom(s.RowVersion))
            .Config;
        foreach (var item in items)
        {
            response.Clients.Add(item.Adapt<ClientDto>(config));
        }
        return response;
    }

    public override async Task<CountRespose> GetCount(Empty request, ServerCallContext context)
    {
        var count = await _mediator.Send(new GetClientCountQuery());
        return new CountRespose { Count = count };
    }

    public override async Task<ClientDto?> GetById(IdRequest request, ServerCallContext context)
    {
        var config = new TypeAdapterConfig().ForType<Client, ClientDto>()
            .Map(d => d.BirthDay, s => Timestamp.FromDateTime(s.BirthDay.ToUniversalTime()))
            .Map(d => d.RowVersion, s => ByteString.CopyFrom(s.RowVersion))
            .Config;
        if (await _mediator.Send(new GetClientByIdQuery(request.Id)) is { } data)
        {
            return data.Adapt<ClientDto>(config);
        }
        return null;
    }

    public override async Task<AddedResponse> Add(ClientDto request, ServerCallContext context)
    {
        var config = new TypeAdapterConfig().ForType<ClientDto, Client>()
            .Map(d => d.BirthDay, s => s.BirthDay.ToDateTime().ToUniversalTime())
            .Map(d => d.RowVersion, s => s.ToByteArray())
            .Config;
        var item = request.Adapt<Client>(config);
        var result = await _mediator.Send(new AddUpdateClientCommand(0, item));
        return new AddedResponse { Id = result };
    }

    public override async Task<SuccessResponse> Update(ClientDto request, ServerCallContext context)
    {
        var config = new TypeAdapterConfig().ForType<ClientDto, Client>()
            .Map(d => d.BirthDay, s => s.BirthDay.ToDateTime().ToUniversalTime())
            .Map(d => d.RowVersion, s => s.ToByteArray())
            .Config;
        var item = request.Adapt<Client>(config);
        var result = await _mediator.Send(new AddUpdateClientCommand(request.Id, item));
        return new SuccessResponse { Success = result > 0 };
    }

    public override async Task<SuccessResponse> Delete(IdRequest request, ServerCallContext context)
    {
        var result = await _mediator.Send(new DeleteClientCommand(request.Id));
        return new SuccessResponse { Success = result };
    }
}
