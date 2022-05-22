namespace RabbitMq1ClientApi.Consumers;

public class GetClientConsumer : IConsumer<IGetClient>
{
    public async Task Consume(ConsumeContext<IGetClient> context)
    {
        Console.WriteLine($"Ответ {context.Message.Value}");

        await context.RespondAsync<IGetClientResult>(new
        {
            context.Message.Value,
            Name = "testName",
        });
    }
}
