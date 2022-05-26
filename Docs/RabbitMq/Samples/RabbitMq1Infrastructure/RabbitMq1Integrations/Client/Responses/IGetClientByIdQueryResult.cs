namespace RabbitMq1Integrations.Client.Responses;

public interface IGetClientByIdQueryResult
{
    public ClientDto? Client { get; set; }
}
