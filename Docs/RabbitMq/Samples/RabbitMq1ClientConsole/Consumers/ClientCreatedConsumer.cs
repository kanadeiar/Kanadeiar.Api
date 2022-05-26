namespace RabbitMq1ClientConsoleApp.Consumers;

public class ClientCreatedConsumer : IConsumer<IClientCreated>
{
    public async Task Consume(ConsumeContext<IClientCreated> context)
    {
        await Task.Run(() =>
        {
            Console.WriteLine($"Получение сообщения от отправителя о создании: номер: {context.Message.Data.Number} название: {context.Message.Data.Model.Name}");
        });
    }
}
