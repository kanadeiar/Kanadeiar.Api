using Mapster;
using Sample.Application.Dtos;
using Sample.Domain.Models;
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
        var count = 3;

        var response = await httpClient.GetAsync($"/api/test/{count}");
        var dtos = await response.EnsureSuccessStatusCode().Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();
        var persons = dtos.Adapt<IEnumerable<Person>>();
        Console.WriteLine($"Response:{Environment.NewLine}");
        foreach (var item in persons)
        {
            Console.WriteLine($"[{item.Id}] {item.SurName} {item.FirstName} {item.Patronymic} {item.BirthDay.ToString("dd.MM.yyyy")}г. {item.Salary.ToString()} {item.Groth.ToString("F2")}");
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }
    Thread.Sleep(5000);
}