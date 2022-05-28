Console.WriteLine("Ожидание старта сервисов");
await Task.Delay(3000);

var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.Host("localhost", h => {
        h.Username("guest");
        h.Password("guest");
    });
    config.ReceiveEndpoint("Lab1ClientConsoleApp", e =>
    {
        e.UseInMemoryOutbox();
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);

try
{
    Console.WriteLine("- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {

        //Console.WriteLine($"Ответ: {response.Message.Client.Id} {response.Message.Client.FirstName}");
        Console.WriteLine($"- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    }
}
finally
{
    await busControl.StopAsync();
}