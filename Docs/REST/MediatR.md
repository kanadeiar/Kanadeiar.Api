# �������� MediatR

[�����](./Index.md)

��� � ���� Application

������� �������� :
```csharp
public class ClientDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public DateTime BirthDay { get; set; }
}
```

������� ���������:
```csharp
public class GetClientById : IRequest<ClientDto?>
{
    public int Id { get; }
    public GetClientById(int id)
    {
        Id = id;
    }
}
```

������� �����������:
```csharp
public class GetClientByIdHandler : IRequestHandler<GetClientById, ClientDto?>
{
    private readonly DbContext _context;
    public GetClientByIdHandler(DbContext context)
    {
        _context = context;
    }

    public async Task<ClientDto?> Handle(GetClientById request, CancellationToken cancellationToken)
    {
        if (await _context.Set<Client>().FindAsync(new object[] { request.Id }, cancellationToken) is Client item)
        {
            return item.Adapt<ClientDto>();
        }
        return null;
    }
}
```