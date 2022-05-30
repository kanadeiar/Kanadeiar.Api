namespace Lab1ClientApi.Consumers;

public class ClientCommandConsumer : IConsumer<AddClientCommand>, IConsumer<UpdateClientCommand>, IConsumer<DeleteClientCommand>
{
    private readonly IMediator _mediator;
    public ClientCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AddClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result > 0)
        {
            await context.Publish<AddClientCommand.IClientAdded>(new { Client = context.Message.Client });
        }
    }

    public async Task Consume(ConsumeContext<UpdateClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.Publish<UpdateClientCommand.IClientUpdated>(new { Id = context.Message.Id, Client = context.Message.Client });
        }
    }

    public async Task Consume(ConsumeContext<DeleteClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.Publish<DeleteClientCommand.IClientDeleted>(new { Id = context.Message.Id });
        }
    }
}
