namespace MT1ClientApi.Consumers.Commands;

/// <summary>
/// Потребитель запросов добавления элемента
/// </summary>
public class AddClientCommandConsumer : IConsumer<AddClientCommand>
{
    private readonly IMediator _mediator;
    public AddClientCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Обработка запросов добавления элемента
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<AddClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result > 0)
        {
            context.Message.Client.Id = result;
            await context.RespondAsync<AddClientCommand.IOk>(new { Id = result });
            await context.Publish<AddClientCommand.IClientAddedEvent>(new { Client = context.Message.Client });
        }
    }
}
