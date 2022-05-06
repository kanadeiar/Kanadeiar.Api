using Google.Protobuf;

Console.WriteLine("Нажмите кнопку для начала:> ");
Console.ReadKey(true);

var channel = GrpcChannel.ForAddress(new Uri("https://localhost:5001"));
var client = new ClientInfo.ClientInfoClient(channel);

while (true)
{
    try
    {
        var count = (await client.GetCountAsync(new Empty())).Count;
        Console.WriteLine("Количество элементов: {0}", count);
        var datas = (await client.GetPagedAsync(new PagedRequest { Offset = 0, Count = 5 })).Clients;
        Console.Write("Несколько: ");
        foreach (var item in datas)
        {
            Console.Write("{0} {1} ", item.Id, item.LastName);
        }
        Console.WriteLine();
        var data = await client.GetByIdAsync(new IdRequest { Id = 1 });
        Console.WriteLine("Один элемент: {0} {1} {2} {3} {4}", data.Id, data.LastName, data.FirstName, data.Patronymic, data.BirthDay.ToDateTime().ToString("dd.MM.yyyy"));
        //var newclient = new ClientDto { UserId = 1, LastName = "Тестов", FirstName = "Тест", Patronymic = "Тестович", BirthDay = Timestamp.FromDateTime(DateTime.UtcNow), RowVersion = ByteString.CopyFrom(new byte[] { }) };
        //var id = (await client.AddAsync(newclient)).Id;
        //var updateclient = await client.GetByIdAsync(new IdRequest { Id = 1 });
        //updateclient.LastName = "Обновленная фамилия";
        //updateclient.FirstName = "Обновленное имя";
        //updateclient.Patronymic = "Обновленное отчество";
        //var success = (await client.UpdateAsync(updateclient)).Success;
        //if (success && await client.GetByIdAsync(new IdRequest { Id = id }) is { } updated)
        //{
        //    Console.WriteLine("Один элемент обновлен: {0} {1} {2} {3} {4}", updated.Id, updated.LastName, updated.FirstName, updated.Patronymic, updated.BirthDay.ToDateTime().ToString("dd.MM.yyyy"));
        //}
        //var successDelete = (await client.DeleteAsync(new IdRequest { Id = id })).Success;
        //if (successDelete)
        //{
        //    Console.WriteLine("Элемент удален с идентификатором: {0}", id);
        //}
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }

    await Task.Delay(5000);
}
