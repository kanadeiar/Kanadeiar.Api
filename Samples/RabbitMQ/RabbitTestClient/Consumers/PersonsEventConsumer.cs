using MassTransit;
using RabbitContracts;

namespace RabbitTestClient.Consumers;

public class PersonsEventConsumer : IConsumer<IGetPersonEvent>
{
    public async Task Consume(ConsumeContext<IGetPersonEvent> context)
    {
        await Task.Run(() => Console.WriteLine($"Получено сообщение о событии - запросе сотрудников {context.Message.GetPersonQuery.Count} штук."));
    }
}
