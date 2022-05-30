namespace gRpc1ClientApplication.Contracts.Commands;

public class DeleteClientCommand : IRequest<bool>
{
    public int Id { get; set; }

    public DeleteClientCommand(int id)
    {
        Id = id;
    }
}
