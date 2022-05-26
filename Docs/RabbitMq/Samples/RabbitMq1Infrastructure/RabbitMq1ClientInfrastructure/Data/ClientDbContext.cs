namespace RabbitMq1ClientInfrastructure.Data;

/// <summary>
/// База данных клиентов
/// </summary>
public class ClientDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    { }
}
