using MediatR;
using RabbitMq1ClientApplication.Contracts.Queries;
using RabbitMq1Integrations.Client.Requests;
using RabbitMq1Integrations.Client.Responses;

namespace RabbitMq1ClientApi.Consumers;

public class ClientConsumer : IConsumer<IGetClientByIdQuery>
{
    private readonly IMediator _mediator;
    public ClientConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<IGetClientByIdQuery> context)
    {
        if (await _mediator.Send(new GetClientByIdQuery(context.Message.Id)) is { } entity)
        {
            await context.RespondAsync<IGetClientByIdQueryResult>(new
            {
                Client = entity,
            });
        }
    }
}
