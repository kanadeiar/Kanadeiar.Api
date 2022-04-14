using MassTransit;
using RabbitPersonsMicroservice.Consumers;

var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.Host("localhost");
    config.ReceiveEndpoint("persons-service", e =>
    {
        e.UseInMemoryOutbox();
        e.Consumer<PersonsConsumer>(c => 
            c.UseMessageRetry(m => m.Interval(5, TimeSpan.FromSeconds(10))));
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);

Console.WriteLine("Сервис сотрудников стартовал");

try
{
    while (true)
    {
        await Task.Delay(10000);
        Console.WriteLine("Сервис сотрудников жив ...");
    }
}
finally
{
    await busControl.StopAsync();
}