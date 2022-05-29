using Lab1ClientConsoleApp;

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
        e.Consumer<ClientCommandConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
    });
});

var source = new CancellationTokenSource(TimeSpan.FromSeconds(60));
await busControl.StartAsync(source.Token);
var clientQueryRequest = new ClientQueryRequest(busControl);

try
{    Console.WriteLine("- Нажмите кнопку для отправки запроса получателю или Q для выхода");

    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        var count = await clientQueryRequest.GetClientCountQuery();
        Console.WriteLine("Всего: {0} элементов", count);
        var datas = await clientQueryRequest.GetPagedClientQuery(0, 5);
        Console.Write("Несколько: ");
        foreach (var item in datas)
        {
            Console.Write("{0} {1} ", item.Id, item.LastName);
        }
        Console.WriteLine();
        var itemById = await clientQueryRequest.GetClientByIdQuery(1);
        if (itemById is { })
        {
            Console.WriteLine("Ответ: {0} {1} {2}", itemById.Id, itemById.FirstName, itemById.Patronymic);
        }
        else
        {
            Console.WriteLine("Элемент не найден");
        }

        var newclient = new Client { UserId = 1, LastName = "Тестов", FirstName = "Тест", Patronymic = "Тестович", BirthDay = DateTime.Today.AddYears(-20), RowVersion = Array.Empty<byte>() };
        await busControl.Publish(new AddClientCommand(newclient));
        while (!StaticClientData.IsAdded)
        {
            await Task.Delay(100);
        }
        StaticClientData.IsAdded = false;
        await busControl.Publish(new UpdateClientCommand(StaticClientData.ClientId, newclient));
        await busControl.Publish(new DeleteClientCommand(StaticClientData.ClientId));
        Console.WriteLine("- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    }
}
finally
{
    await busControl.StopAsync();
}