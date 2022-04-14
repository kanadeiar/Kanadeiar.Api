using MassTransit;
using RabbitContracts;
using RabbitTestClient.Consumers;

Console.WriteLine("Ожидание старта сервисов");
await Task.Delay(2000);

var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.Host("localhost");
    config.ReceiveEndpoint("persons-test", e => 
    {
        e.UseInMemoryOutbox();
        e.Consumer<PersonsEventConsumer>(c =>
            c.UseMessageRetry(m => m.Interval(5, TimeSpan.FromSeconds(10))));
    });    
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);
var client = busControl.CreateRequestClient<IGetPersonRequest>(TimeSpan.FromSeconds(10));

try
{
    //IRequestClient<IGetPersonRequest> client = CreateRequestClient(busControl);
    Console.WriteLine("Нажмите кнопку для отправки запроса получателю и Q для выхода");
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        await SendRequestForInvoiceCreated(busControl, client);
        Console.WriteLine("Нажмите кнопку для отправки запроса получателю и Q для выхода");
    }
}
finally
{
    await busControl.StopAsync();
}

static async Task SendRequestForInvoiceCreated(IPublishEndpoint endpoint, IRequestClient<IGetPersonRequest> client)
{
    var response = await client.GetResponse<IGetPersonResult>(new { Count = 3 });

    var persons = response.Message.Persons;

    foreach (var item in persons)
    {
        Console.WriteLine($"{item.Id} {item.SurName} {item.FirstName} {item.BirthDay}");
    }
    //await endpoint.Publish<IGetPersonRequest>(new
    //{
    //    Count = 10,
    //});
}

static IRequestClient<IGetPersonRequest> CreateRequestClient(IBusControl busControl)
{
    //var serviceAddress = new Uri("localhost");
    //IRequestClient<IGetPersonRequest> client =
    //    busControl.CreateRequestClient<IGetPersonRequest>(serviceAddress, TimeSpan.FromSeconds(10));
    var client = busControl.CreateRequestClient<IGetPersonRequest>(TimeSpan.FromSeconds(10));
    return client;
}