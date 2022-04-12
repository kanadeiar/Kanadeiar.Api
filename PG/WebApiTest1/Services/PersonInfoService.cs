using Grpc.Core;

namespace WebApiTest1.Services;

public class PersonInfoService : PersonInform.PersonInformBase
{
    private readonly ILogger<PersonInfoService> _logger;

    public PersonInfoService(ILogger<PersonInfoService> logger)
    {
        _logger = logger;
    }

    public override async Task<PersonsResponse> GetPersons(PersonRequest request, ServerCallContext context)
    {
        await Task.Delay(1000);
        var persons = Enumerable.Range(0, request.Count).Select(i => new Person
        {
            Id = i,
            Surname = $"Иванов_{i}",
            Firstname = $"Иван_{i}",
            Age = 18 + i,
        }).ToArray();
        var result = new PersonsResponse();
        foreach (var item in persons)
        {
            result.Persons.Add(item);
        }
        _logger.LogInformation("Нормальное успешное получение данных");
        return result;
    }
}
