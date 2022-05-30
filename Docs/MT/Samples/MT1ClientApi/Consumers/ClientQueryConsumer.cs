namespace MT1ClientApi.Consumers;

/// <summary>
/// Потребитель запросов клиентов
/// </summary>
public class ClientQueryConsumer : IConsumer<GetPagedClientQuery>, IConsumer<GetClientCountQuery>, IConsumer<GetClientByIdQuery> 
{
    private readonly IMediator _mediator;
    public ClientQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получение элементов с постраничной разбивкой
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<GetPagedClientQuery> context)
    {
        var items = await _mediator.Send(context.Message);
        await context.RespondAsync<GetPagedClientQuery.IOk>(new { Clients = items });
    }

    /// <summary>
    /// Получение количества элементов
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Consume(ConsumeContext<GetClientCountQuery> context)
    {
        var count = await _mediator.Send(context.Message);
        await context.RespondAsync<GetClientCountQuery.IOk>(new { Count = count });
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
