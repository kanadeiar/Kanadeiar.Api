using Sample.Domain.Models;

namespace RabbitContracts;

public interface IGetPersonRequest
{
    int Count { get; set; }
}

public interface IGetPersonResult
{
    IList<Person> Persons { get; set; }
}

public interface IGetPersonEvent
{
    IGetPersonRequest GetPersonQuery { get; set; }
    IGetPersonResult GetPersonResult { get; set; }
}
