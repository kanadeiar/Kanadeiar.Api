namespace Rest1ClientInfrastructure.Repositories;

/// <summary>
/// Реализация репозитория клиентов
/// </summary>
public class ClientRepository : KndRepositoryAsync<Client, int>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }
}
