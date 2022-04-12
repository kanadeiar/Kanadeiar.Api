Console.WriteLine("Нажать на кнопку для запуска");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress("https://localhost:6001");
var client = new Persons.PersonsClient(channel);

while (true)
{
    try
    {
        var response = await client.GetPersonsAsync(new PersonsRequest { Count = 3 });
        foreach (var item in response.Persons)
        {
            Console.WriteLine("{0} - {1} {2} {3} {4}, ", item.Id, item.SurName, item.FirstName, item.Patronymic, item.BirthDay);
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("{0}", ex.Message);
    }
}
