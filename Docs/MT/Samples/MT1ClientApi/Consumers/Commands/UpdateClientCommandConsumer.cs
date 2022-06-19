namespace MT1ClientApi.Consumers.Commands;

/// <summary>
/// Потребитель запросов о обновлении элемента
/// </summary>
public class UpdateClientCommandConsumer : IConsumer<UpdateClientCommand>
{
    private readonly IMediator _mediator;
    public UpdateClientCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Обработка запроса о обновлении элемента
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<UpdateClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.RespondAsync<UpdateClientCommand.IOk>(new { Success = result });
            await context.Publish<UpdateClientCommand.IClientUpdatedEvent>(new { Id = context.Message.Id, Client = context.Message.Client });
        }
    }
}
