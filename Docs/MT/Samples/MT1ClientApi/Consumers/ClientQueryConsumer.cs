namespace MT1ClientApi.Consumers;

/// <summary>
/// Потребитель запросов клиентов
/// </summary>
public class ClientQueryConsumer : IConsumer<GetClientByIdQuery>
{
    private readonly IMediator _mediator;
    public ClientQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение одного элемента
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<GetClientByIdQuery> context)
    {
        if (await _mediator.Send(context.Message) is { } entity)
        {
            await context.RespondAsync<GetClientByIdQuery.IOk>(new { Client = entity });
        }
        else
        {
            await context.RespondAsync<GetClientByIdQuery.INotFound>(new { });
        }
    }
}
