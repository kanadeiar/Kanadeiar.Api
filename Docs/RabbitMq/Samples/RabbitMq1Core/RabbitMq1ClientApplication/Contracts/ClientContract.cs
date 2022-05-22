namespace RabbitMq1ClientApplication.Contracts;

public class ClientContract
{
    public string Name { get; set; }
}

public interface IClientToCreate
{
    int Number { get; set; }
    ClientContract Model { get; set; }
}

public interface IClientCreated
{
    int Number { get; set; }
    IClientToCreate Data { get; set; }
}
