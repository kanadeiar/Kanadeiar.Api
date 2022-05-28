namespace Lab1ClientApi.Consumers;

public class ClientQueryConsumer : IConsumer<GetClientByIdQuery>, IConsumer<GetClientCountQuery>, IConsumer<GetPagedClientQuery>
{
    private readonly IMediator _mediator;
    public ClientQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<GetPagedClientQuery> context)
    {
        var items = await _mediator.CreateStream(new GetPagedClientQuery(context.Message.Offset, context.Message.Count)).ToListAsync();
        await context.RespondAsync<GetPagedClientQuery.IOk>(new { Clients = items });
    }

    public async Task Consume(ConsumeContext<GetClientCountQuery> context)
    {
        var count = await _mediator.Send(context.Message);
        await context.RespondAsync<GetClientCountQuery.IOk>(new { Count = count });
    }

    public async Task Consume(ConsumeContext<GetClientByIdQuery> context)
    {
        if (await _mediator.Send(context.Message) is Client entity)
        {
            await context.RespondAsync<GetClientByIdQuery.IOk>(new { Client = entity });
        }
        else
        {
            await context.RespondAsync<GetClientByIdQuery.INotFound>(new { });
        }        
    }
}
