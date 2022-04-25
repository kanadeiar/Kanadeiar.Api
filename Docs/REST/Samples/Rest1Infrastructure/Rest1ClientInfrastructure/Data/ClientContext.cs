namespace Rest1ClientInfrastructure.Data;

/// <summary>
/// База данных клиентов
/// </summary>
public class ClientContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// База данных клиентов
    /// </summary>
    /// <param name="options"></param>
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    { }
}
