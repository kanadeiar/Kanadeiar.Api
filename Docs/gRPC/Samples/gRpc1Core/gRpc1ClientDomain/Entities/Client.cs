namespace gRpc1ClientDomain.Entities;

public class Client : Entity
{
    public int UserId { get; set; }

    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(100)]
    public string FirstName { get; set; }

    [MaxLength(100)]
    public string Patronymic { get; set; }

    public DateTime BirthDay { get; set; }
}
