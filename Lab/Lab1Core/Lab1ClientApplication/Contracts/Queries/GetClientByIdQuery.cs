namespace Lab1ClientApplication.Contracts.Queries;

public class GetClientByIdQuery : IRequest<Client?>
{
    public int Id { get; }

    public GetClientByIdQuery(int id)
    {
        Id = id;
    }

    public interface IOk
    {
        Client Client { get; set; }
    }

    public interface IError
    {
        string Message { get; set; }
    }
}
