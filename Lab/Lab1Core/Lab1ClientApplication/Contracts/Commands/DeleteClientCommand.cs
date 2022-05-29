namespace Lab1ClientApplication.Contracts.Commands;

public class DeleteClientCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteClientCommand(int id)
    {
        Id = id;
    }

    public interface IClientDeleted
    {
        public int Id { get; set; }
    }
}
