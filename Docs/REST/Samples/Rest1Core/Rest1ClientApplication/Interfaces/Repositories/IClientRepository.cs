using Kanadeiar.Api.Interfaces.Repositories;

namespace Rest1ClientApplication.Interfaces.Repositories;

public interface IClientRepository : IKndRepositoryAsync<Client, int>
{
}
