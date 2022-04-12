namespace WebApiTest1.Services;

public class PersonService
{
    public async Task<IEnumerable<Person>> GetPersons(int count)
    {
        await Task.Delay(1000);
        var persons = Enumerable.Range(0, count).Select(i => new Person
        {
            Id = i,
            Surname = $"Иванов_{i}",
            Firstname = $"Иван_{i}",
            Age = 18 + i,
        }).ToArray();
        return persons;
    }
}
