# �����������

[�����](./Index.md)

> ������ ���� �������� ����

�������� ����� � Domain:
```sharp
dotnet add package Kanadeiar.Core
```
�������� ����� � Application:
```sharp
dotnet add package Kanadeiar.Api
```

## ����������� ��������� � Domain

�������� ���������� ���������� IKndEntity � ������� ��������, � ��������-��� ���������� ��� �����
```sharp
abstract public class Entity : IKndEntity<int>
{
    [Key]
    public int Id { get; set; }
}
```

## ����������� ����������� � Application

���������� ��������� ������������� � ���� ���� ����������� ����������� IKndRepositoryAsync, ��� � ���������-���� ���������� ��� ������� ����������� ��� �������� � ��� ��������� ����
```sharp
public interface IClientRepository : IKndRepositoryAsync<Client, int>
{
}
```

������������ ����������� - �������:
```sharp
_clientRepository.Query()
await _clientRepository.GetPagedAsync(offset, count, cancellationToken)
await _clientRepository.GetByIdAsync(request.Id)
```
�������:
```sharp
var result = await _clientRepository.AddAsync(entity);
await _clientRepository.UpdateAsync(entity);
await _clientRepository.DeleteAsync(entity);
var count = await _clientRepository.CommitAsync(CancellationToken cancellationToken);
```

## ���������� ����������� � Infrastructure

���������� �����������:
```sharp
public class ClientRepository : KndRepositoryAsync<Client, int>, IClientRepository
{
    public ClientRepository(DbContext context) : base(context)
    {
    }
}
```
����������� ���������� ������� ����������, ��� ������������� - ���������, ������� � ����������-����� �������� � ��� ��������� ����

� ������������ - �������� �������� ���� ������

## ����������� ������������ � �������� ����������:

� ���� Infrastructure
```sharp
public static IServiceCollection MyAddRepositories(this IServiceCollection services)
{
    services.AddScoped<IClientRepository, ClientRepository>();

    return services;
}
```

� �������� ����������:

```sharp
builder.Services.MyAddRepositories();
```
