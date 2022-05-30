namespace MT1ClientApplication.Contracts.Commands;

/// <summary>
/// Добавление нового элемента
/// </summary>
public class AddClientCommand : IRequest<int>
{
    /// <summary>
    /// Новый элемент
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// Добавление нового элемента
    /// </summary>
    /// <param name="client"></param>
    public AddClientCommand(Client client)
    {
        Client = client;
    }

    /// <summary>
    /// Уведомление о добавлении нового элемента
    /// </summary>
    public interface IClientAdded
    {
        Client Client { get; set; }
    }
}
