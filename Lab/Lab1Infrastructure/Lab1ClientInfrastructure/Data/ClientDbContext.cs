namespace Lab1ClientInfrastructure.Data;

public class ClientDbContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();

    public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
    { }
}
