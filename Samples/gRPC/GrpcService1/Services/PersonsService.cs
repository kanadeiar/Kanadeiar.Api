namespace GrpcService1.Services;

public class PersonsService : Persons.PersonsBase
{
    private readonly ILogger<PersonsService> _logger;
    private readonly GenPersonService _personService;

    public PersonsService(ILogger<PersonsService> logger, GenPersonService personService)
    {
        _logger = logger;
        _personService = personService;
    }

    public override async Task<PersonsResult> GetPersons(PersonsRequest request, ServerCallContext context)
    {
        var persons = await _personService.GetPersons(request.Count);
        var result = new PersonsResult();
        foreach (var item in persons)
        {
            var person = item.Adapt<Person>();
            result.Persons.Add(person);
        }
        return result;
    }
}
