namespace Lab1ClientInfrastructure.Repositories;

public class ClientRepository : KndRepositoryAsync<Client, int>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }
}
