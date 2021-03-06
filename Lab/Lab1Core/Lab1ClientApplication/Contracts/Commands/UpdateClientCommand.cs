namespace Lab1ClientApplication.Contracts.Commands;

public class UpdateClientCommand : IRequest<bool>
{
    public int Id { get; set; }
    public Client Client { get; set; }

    public UpdateClientCommand(int id, Client client)
    {
        Id = id;
        Client = client;
    }

    public interface IClientUpdated
    {
        public int Id { get; set; }
        Client Client { get; set; }
    }
}
