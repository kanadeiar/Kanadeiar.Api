using MassTransit;
using RabbitContracts;
using Sample.Application.Services;

namespace RabbitPersonsMicroservice.Consumers;

public class PersonsConsumer : IConsumer<IGetPersonRequest>
{
    GenPersonService _service;

    public PersonsConsumer()
    {
        _service = new GenPersonService();
    }

    public async Task Consume(ConsumeContext<IGetPersonRequest> context)
    {
        var persons = await _service.GetPersons(context.Message.Count);
        if (persons == null || !persons.Any())
            throw new InvalidOperationException("Persons not count");

        Console.WriteLine($"Ответ на запрос о сотрудниках в количестве {context.Message.Count} штук");

        //ответ
        await context.RespondAsync<IGetPersonResult>(new
        {
            Persons = persons,
        });

        //уведомляем остальных
        await context.Publish<IGetPersonEvent>(new
        {
            GetPersonQuery = context.Message,
            GetPersonResult = new
            {
                Persons = persons,
            }
        });
    }
}
