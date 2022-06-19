namespace MT1ClientApi.Consumers.Queries;

/// <summary>
/// Потребитель запроса получения эелементов с костраничной разбивкой
/// </summary>
public class GetPagedClientQueryConsumer : IConsumer<GetPagedClientQuery>
{
    private readonly IMediator _mediator;
    public GetPagedClientQueryConsumer(IMediator mediator)
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
}
