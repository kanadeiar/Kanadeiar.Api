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
    var clientCountQuery = busControl.CreateRequestClient<GetClientCountQuery>();
    var clientPagedQuery = busControl.CreateRequestClient<GetPagedClientQuery>();
    var clientByIdQuery = busControl.CreateRequestClient<GetClientByIdQuery>();
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        var count = await clientCountQuery.GetResponse<GetClientCountQuery.IOk>(new GetClientCountQuery());
        Console.WriteLine("Всего: {0} элементов", count.Message.Count);
        var datas = await clientPagedQuery.GetResponse<GetPagedClientQuery.IOk>(new GetPagedClientQuery(0, 5));
        Console.Write("Несколько: ");
        foreach (var item in datas.Message.Clients)
        {
            Console.Write("{0} {1} ", item.Id, item.LastName);
        }
        Console.WriteLine();
        var (ok, _) = await clientByIdQuery.GetResponse<GetClientByIdQuery.IOk, GetClientByIdQuery.INotFound>(new GetClientByIdQuery(1));
        if (ok.IsCompletedSuccessfully)
        {
            var item = (await ok).Message.Client;
            Console.WriteLine("Ответ: {0} {1} {2}", item.Id, item.FirstName, item.Patronymic);
        }
        else
        {
            Console.WriteLine("Элемент не найден");
        }

        Console.WriteLine("- Нажмите кнопку для отправки запроса получателю или Q для выхода");
    }
}
finally
{
    await busControl.StopAsync();
}