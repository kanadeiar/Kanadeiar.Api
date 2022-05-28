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
    var client = busControl.CreateRequestClient<GetClientByIdQuery>();
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        var (ok, _) = await client.GetResponse<GetClientByIdQuery.IOk, GetClientByIdQuery.IError>(new GetClientByIdQuery(1));
        if (ok.IsCompletedSuccessfully)
        {
            var item = (await ok).Message.Client;
            Console.WriteLine($"Ответ: {item.Id} {item.FirstName} {item.Patronymic}");
        }
        else
        {
            Console.WriteLine("Элемент не найден");
        }

        Console.WriteLine($"- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    }
}
finally
{
    await busControl.StopAsync();
}