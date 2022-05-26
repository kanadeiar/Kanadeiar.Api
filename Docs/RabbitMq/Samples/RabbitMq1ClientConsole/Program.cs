Console.WriteLine("Ожидание старта сервисов");
await Task.Delay(3000);

var busControl = Bus.Factory.CreateUsingRabbitMq(config =>
{
    config.Host("localhost", "/", h => {
        h.Username("guest");
        h.Password("guest");
    });
    config.ReceiveEndpoint("RabbitMq1ClientConsoleApp", e => 
    {
        e.UseInMemoryOutbox();
        e.Consumer<ClientCreatedConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);

var keyCount = 0;
try
{
    Console.WriteLine("- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        keyCount++;
        await CreateCommand.SendRequestForInvoiceCreated(busControl, keyCount, $"Name_{keyCount}");
        var response = await GetQuery.SendQueryGetClient(busControl, keyCount);
        Console.WriteLine($"Ответ: {response.Value} {response.Name}");        
        Console.WriteLine($"- Нажмите кнопку для отправки запроса получателю или Q для выхода, количество раз: {keyCount}");
    }
}
finally
{
    await busControl.StopAsync();
}



