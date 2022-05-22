namespace RabbitMq1ClientApplication.Contracts;

public interface IGetClient
{
    int Value { get; set; }
}

public interface IGetClientResult
{
    int Value { get; set; }
    string Name { get; set; }
}
