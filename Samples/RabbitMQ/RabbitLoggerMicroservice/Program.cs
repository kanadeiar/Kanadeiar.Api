using MassTransit;
using RabbitLoggerMicroservice.Consumers;

var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.Host("localhost");
    config.ReceiveEndpoint("persons-logger-service", e =>
    {
        e.Consumer<PersonsEventConsumer>(c =>
            c.UseMessageRetry(m => m.Interval(5, TimeSpan.FromSeconds(10))));
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);
Console.WriteLine("Сервис логирования стартовал");

try
{
    while (true)
    {
        await Task.Delay(10000);
        Console.WriteLine("Сервис логирования жив");
    }
}
finally
{
    await busControl.StopAsync();
}