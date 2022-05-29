namespace Lab1ClientApplication.Contracts.Commands;

public class AddClientCommand : IRequest<int>
{
    public Client Client { get; set; }
    
    public AddClientCommand(Client client)
    {
        Client = client;
    }

    public interface IClientAdded
    {
        Client Client { get; set; }
    }
}
