namespace Rest1ClientInfrastructure.Repositories;

/// <summary>
/// Реализация репозитория клиентов
/// </summary>
public class ClientRepository : RepositoryAsync<Client>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }


}
