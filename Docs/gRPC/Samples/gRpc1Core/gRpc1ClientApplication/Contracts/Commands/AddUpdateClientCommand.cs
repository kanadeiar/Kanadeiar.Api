namespace gRpc1ClientApplication.Contracts.Commands;

public class AddUpdateClientCommand : IRequest<int>
{
    public int Id { get; }

    public Client Client { get; }

    public AddUpdateClientCommand(int id, Client client)
    {
        Id = id;
        Client = client;
    }
}
