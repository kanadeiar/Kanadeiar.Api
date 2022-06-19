namespace MT1ClientApi.Consumers.Queries;

/// <summary>
/// Потребитель запросов получения количества элементов
/// </summary>
public class GetClientCountQueryConsumer : IConsumer<GetClientCountQuery>
{
    private readonly IMediator _mediator;
    public GetClientCountQueryConsumer(IMediator mediator)
    {
        _mediator = mediator;
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
}
