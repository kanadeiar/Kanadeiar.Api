namespace Lab1ClientApi.Consumers;

public class ClientConsumer : IConsumer<GetClientByIdQuery>
{
    private readonly IMediator _mediator;
    public ClientConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<GetClientByIdQuery> context)
    {
        if (await _mediator.Send(context.Message) is GetClientByIdQueryResult result)
        {
            await context.RespondAsync(result);
        }
    }
}
