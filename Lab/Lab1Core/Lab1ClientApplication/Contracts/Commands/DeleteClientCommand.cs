namespace Lab1ClientApplication.Contracts.Commands;

public class DeleteClientCommand : IRequest<bool>
{
    public int Id { get; }

    public DeleteClientCommand(int id)
    {
        Id = id;
    }

    public interface IOk
    {
        public bool Success { get; set; }
    }

    public interface IError
    { }
}
