namespace MT1ClientInfra.Repos;

public class ClientRepo : KndRepositoryAsync<Client, int>, IClientRepo
{
    public ClientRepo(DbContext context) : base(context)
    {
    }
}
