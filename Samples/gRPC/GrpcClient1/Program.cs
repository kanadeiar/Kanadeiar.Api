
using Grpc.Net.Client;
using GrpcService1;

Console.WriteLine("Нажать на кнопку для запуска");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress("https://localhost:6001");
var client = new Persons.PersonsClient(channel);

while (true)
{
    try
    {
        var response = await client.GetPersonsAsync(new PersonsRequest { Count = 10 });
        foreach (var item in response.Persons)
        {
            Console.Write("{0} - {1} {2} {3}, ", item.Id, item.Surname, item.Firstname, item.Age);
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("{0}", ex.Message);
    }
}
