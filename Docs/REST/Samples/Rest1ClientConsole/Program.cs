Console.WriteLine("Нажмите кнопку для начала:> ");
Console.ReadKey(true);

HttpClient httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:6001");
IClientApiClient client = new ClientApiClient(httpClient);

while (true)
{
    try
    {
        var count = await client.GetCountAsync();
        Console.WriteLine("Всего: {0} элементов", count);
        var datas = await client.GetPagedAsync(0, 5);
        Console.Write("Несколько: ");
        foreach (var item in datas)
        {
            Console.Write("{0} {1} ", item.Id, item.LastName);
        }
        Console.WriteLine();
        if (await client.GetByIdAsync(1) is { } data)
        {
            Console.WriteLine("Один элемент: {0} {1} {2} {3} {4}", data.Id, data.LastName, data.FirstName, data.Patronymic, data.BirthDay.ToString("dd.MM.yyyy"));
        }
        else
        {
            Console.WriteLine("Элемент не найден");
        }
        var newclient = new Client { UserId = 1, LastName = "Тестов", FirstName = "Тест", Patronymic = "Тестович", BirthDay = DateTime.Today.AddYears(-20), RowVersion = Array.Empty<byte>() };
        var id = await client.AddAsync(newclient);
        Console.WriteLine("Клиент добавлен, идентификатор: {0}", id);
        var updateclient = await client.GetByIdAsync(id);
        updateclient.LastName = "Обновленная фамилия";
        updateclient.FirstName = "Обновленное имя";
        updateclient.Patronymic = "Обновленное отчество";
        var success = await client.UpdateAsync(updateclient);
        if (success && await client.GetByIdAsync(id) is { } updated)
        {
            Console.WriteLine("Один элемент обновлен: {0} {1} {2} {3} {4}", updated.Id, updated.LastName, updated.FirstName, updated.Patronymic, updated.BirthDay.ToString("dd.MM.yyyy"));
        }
        var successDelete = await client.DeleteAsync(id);
        if (successDelete)
        {
            Console.WriteLine("Элемент удален с идентификатором: {0}", id);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }

    await Task.Delay(5000);
}


