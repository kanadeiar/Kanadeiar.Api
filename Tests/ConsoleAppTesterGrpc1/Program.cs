using Grpc.Net.Client;

Console.WriteLine("Нажми клавишу для начала тестирования связи gRPC с сервисом:");
Console.ReadKey();

using var channel = GrpcChannel.ForAddress(new Uri("https://localhost:5001"));
var client = new PersonInform.PersonInformClient(channel);

while (true)
{
    try
    {
        var count = 10;

        var response = await client.GetPersonsAsync(new PersonRequest { Count = 10 });
        Console.WriteLine($"Response:{Environment.NewLine}");
        foreach (var item in response.Persons)
        {
            Console.Write($"[{item.Id}] {item.Surname} {item.Firstname} {item.Age}");
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }
    Thread.Sleep(5000);
}

