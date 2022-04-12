namespace Sample.Application.Services;

public class GenPersonService
{
    public async Task<IEnumerable<PersonDto>> GetPersons(int count)
    {
        await Task.Delay(1000);
        var persons = Enumerable.Range(0, count).Select(i => new Person
        {
            Id = i,
            SurName = $"Иванов_{i}",
            FirstName = $"Иван_{i}",
            Patronymic = $"Иванович_{i}",
            BirthDay = DateTime.Today.AddYears(-20).AddDays(i),
            Salary = 199.3M + i,
            Groth = 123.3 + i,
            DepartmentId = 1,
        }).ToArray();
        return persons.Adapt<IEnumerable<PersonDto>>();
    }
}
