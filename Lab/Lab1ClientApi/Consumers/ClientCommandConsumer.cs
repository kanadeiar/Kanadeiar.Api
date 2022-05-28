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
            await context.RespondAsync<AddClientCommand.IOk>(new { Id = result });
        }
        else
        {
            await context.RespondAsync<AddClientCommand.IError>(new { });
        }
    }

    public async Task Consume(ConsumeContext<UpdateClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.RespondAsync<UpdateClientCommand.IOk>(new { Success = result });
        }
        else
        {
            await context.RespondAsync<UpdateClientCommand.IError>(new { });
        }
    }

    public async Task Consume(ConsumeContext<DeleteClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.RespondAsync<DeleteClientCommand.IOk>(new { Success = result });
        }
        else
        {
            await context.RespondAsync<DeleteClientCommand.IError>(new { });
        }
    }
}
