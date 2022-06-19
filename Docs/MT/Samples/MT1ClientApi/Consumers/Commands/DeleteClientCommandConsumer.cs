namespace MT1ClientApi.Consumers.Commands;

/// <summary>
/// Потребитель запросов о удалении элементов
/// </summary>
public class DeleteClientCommandConsumer : IConsumer<DeleteClientCommand>
{
    private readonly IMediator _mediator;
    public DeleteClientCommandConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Обработка запроса о удалении элементов
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<DeleteClientCommand> context)
    {
        var result = await _mediator.Send(context.Message);
        if (result)
        {
            await context.RespondAsync<DeleteClientCommand.IOk>(new { Success = result });
            await context.Publish<DeleteClientCommand.IClientDeletedEvent>(new { Id = context.Message.Id });
        }
    }
}
