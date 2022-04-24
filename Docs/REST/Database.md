# ���� ������ EF

[�����](./Index.md)

�������� ������ � ����������:
```sharp
dotnet add package Microsoft.EntityFrameworkCore.Design
```
�������� ����� � ���������������� ����������:
```sharp
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

������� ����������� ���� ������:
```sharp
public class ClientContext : DbContext
{
    public DbSet<Client> Clients => Set<Client>();
    public ClientContext(DbContextOptions<ClientContext> options) : base(options)
    { }
}
```

������ ����������� � ����� ������������ ���������� appsettings.json:
```xml
"ConnectionString": "Server=(localdb)\\MSSQLLocalDB;Database=ClientService;MultipleActiveResultSets=True;Integrated Security=true"
```

������� ����������� � �������� ����������:
```sharp
services.AddDbContext<ClientContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionString"),
        o => o.MigrationsAssembly("Rest1ClientInfrastructure")); 
#if DEBUG
    options.EnableSensitiveDataLogging(); // ������ � ���� ��� ��������
#endif
});

services.AddScoped<DbContext, ClientContext>(); //���� ���� ���� ������ � ����������
```

> ���� �������� ��������� � ���-�� �����, ��� � ����������� ���� ������, �� ����� �� �������� ��������������� �������� - MigrationsAssembly()

## ������� �������� ��������

������� �������:
```cmd
dotnet ef migrations add init
```

���� ��������� � ��������� ���������� �� ������� ����������, �� ������� ������� c ��������� ���������:

```cmd
dotnet ef --startup-project ../../Rest1ClientApi/ migrations add init --context ClientContext
```
