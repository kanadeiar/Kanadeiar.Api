namespace RabbitMq1ClientApi.Consumers;

public class ClientToCreateConsumer : IConsumer<IClientToCreate>
{
    public async Task Consume(ConsumeContext<IClientToCreate> context)
    {
        var number = new Random().Next(10000, 99999);

        Console.WriteLine($"Создание счета {number} для получателя: {context.Message.Number}");
        Console.WriteLine($"Модель - название: {context.Message.Model.Name}");

        await context.Publish<IClientCreated>(new
        {
            Number = context.Message.Number,
            Data = new
            {
                context.Message.Number,
                context.Message.Model,
            }
        });
    }
}
