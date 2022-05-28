namespace Lab1ClientApplication.Contracts.QueriesResults;

public class GetClientByIdQueryResult
{
    public Client Client { get; set; }
    
    public GetClientByIdQueryResult(Client client)
    {
        Client = client;
    }
}
