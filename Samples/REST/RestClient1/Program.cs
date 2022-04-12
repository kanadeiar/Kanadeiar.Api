using Microsoft.Extensions.Configuration;
using RestServer1.Models;
using System.Net.Http.Json;

Console.WriteLine("Нажми клавишу для начала тестирования связи с сервисом:");
Console.ReadKey();
Console.WriteLine();

HttpClient httpClient = new HttpClient();
string mapInfoUrl = "https://localhost:6001";
httpClient.BaseAddress = new Uri(mapInfoUrl);

while (true)
{
    try
    {
        var count = 10;

        var response = await httpClient.GetAsync($"/api/test/{count}");
        var persons = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<Person>>();

        Console.WriteLine($"Response:{Environment.NewLine}");
        foreach (var item in persons)
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