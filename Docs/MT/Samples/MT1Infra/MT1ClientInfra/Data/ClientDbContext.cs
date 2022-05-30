namespace MT1ClientInfra.Data;

/// <summary>
/// База данных
/// </summary>
public class ClientDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    { }
}
