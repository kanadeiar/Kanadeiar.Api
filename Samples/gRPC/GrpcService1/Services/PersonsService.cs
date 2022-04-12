using Grpc.Core;

namespace GrpcService1.Services
{
    public class PersonsService : Persons.PersonsBase
    {
        private readonly ILogger<PersonsService> _logger;
        public PersonsService(ILogger<PersonsService> logger)
        {
            _logger = logger;
        }

        public override async Task<PersonsResult> GetPersons(PersonsRequest request, ServerCallContext context)
        {
            await Task.Delay(1000);
            var persons = Enumerable.Range(0, request.Count).Select(i => new Person
            {
                Id = i,
                Surname = $"Иванов_{i}",
                Firstname = $"Иван_{i}",
                Age = 18 + i,
            }).ToArray();
            var result = new PersonsResult();
            foreach (var item in persons)
            {
                result.Persons.Add(item);
            }
            return result;
        }
    }
}