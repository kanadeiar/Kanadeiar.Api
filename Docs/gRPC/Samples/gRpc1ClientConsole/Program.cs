Console.WriteLine("Нажмите кнопку для начала:> ");
Console.ReadKey(true);

var channel = GrpcChannel.ForAddress(new Uri("https://localhost:5001"));
var client = new ClientInfo.ClientInfoClient(channel);

while (true)
{
    try
    {
        var data = await client.GetByIdAsync(new IdRequest { Id = 1 });
        Console.WriteLine("Один элемент: {0} {1} {2} {3} {4}", data.Id, data.LastName, data.FirstName, data.Patronymic, data.BirthDay.ToDateTime().ToString("dd.MM.yyyy"));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }

    await Task.Delay(5000);
}
