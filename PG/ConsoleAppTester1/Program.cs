using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using WebApiTest1.Models;
using Mapster;

var builder = new ConfigurationBuilder()
  .SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json")
  .AddEnvironmentVariables()
  .AddUserSecrets(typeof(Program).Assembly, optional: true)
  .AddCommandLine(args);
IConfigurationRoot configuration = builder.Build();

Console.WriteLine("Нажми клавишу для начала тестирования HTTP REST связи с сервисом:");
Console.ReadKey();

HttpClient httpClient = new HttpClient();
string mapInfoUrl = configuration.GetValue<string>("MapInfoUrl");
httpClient.BaseAddress = new Uri(mapInfoUrl);

while (true)
{
    try
    {
        var count = 10;

        var response = await httpClient.GetAsync($"/api/test/{count}");
        var dtos = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();
        var persons = dtos.Adapt<IEnumerable<Person>>();
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
