

using System.Net.Http.Json;

Console.WriteLine("Нажми клавишу для начала тестирования связи с сервисом:");
Console.ReadKey(true);
Console.WriteLine();

HttpClient httpClient = new HttpClient();
string mapInfoUrl = "https://localhost:6001";
httpClient.BaseAddress = new Uri(mapInfoUrl);

while (true)
{
    try
    {
        var response = await httpClient.GetAsync($"/value?value=Test");
        var message = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
        Console.WriteLine($"Ответ с сервера: {message}");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"{ex.Message}\n{ex.StackTrace}");
    }
    Thread.Sleep(5000);
}