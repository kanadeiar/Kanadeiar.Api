Console.WriteLine("Ожидание старта сервисов");
await Task.Delay(3000);

HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7001");
IClientApiClient client = new ClientApiClient(httpClient);

Console.WriteLine("* Нажмите кнопку для отправки запроса получателю или Q для выхода");
try
{
    while (Console.ReadKey(true).Key != ConsoleKey.Q)
    {
        var count = await client.GetCountAsync();
        Console.WriteLine($"Количетсво элементов: {count}");
        var items = await client.GetPagedAsync(0, 5);
        Console.Write("Несколько: ");
        foreach (var item in items)
        {
            Console.Write("{0} {1} ", item.Id, item.LastName);
        }
        Console.WriteLine();
        try
        {
            var data = await client.GetByIdAsync(1);
            Console.WriteLine("Один элемент: {0} {1} {2} {3} {4}", data.Id, data.LastName, data.FirstName, data.Patronymic, data.BirthDay.ToString("dd.MM.yyyy"));
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine("Элемент не найден");
        }

        var newclient = new Client { UserId = 1, LastName = "Тестов", FirstName = "Тест", Patronymic = "Тестович", BirthDay = DateTime.Today.AddYears(-20), RowVersion = Array.Empty<byte>() };
        var newid = await client.AddAsync(newclient);
        Console.WriteLine($"Добавлен элемент с новым ид: {newid}");
        var updating = await client.GetByIdAsync(newid);
        updating.FirstName = "Обновленное имя";
        if (await client.UpdateAsync(updating))
        {
            Console.WriteLine($"Элемент обновлен");
        }
        if (await client.DeleteAsync(newid))
        {
            Console.WriteLine("Элемент успешно удален");
        }

        Console.WriteLine("* Нажмите кнопку для отправки запроса получателю или Q для выхода");
    }
}
catch
{
    Console.WriteLine("Неизвестная ошибка");
    throw;
}
