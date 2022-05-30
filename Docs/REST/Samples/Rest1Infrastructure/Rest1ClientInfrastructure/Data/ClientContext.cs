namespace Rest1ClientInfrastructure.Data;

/// <summary>
/// База данных клиентов
/// </summary>
public class ClientContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    { }
}
