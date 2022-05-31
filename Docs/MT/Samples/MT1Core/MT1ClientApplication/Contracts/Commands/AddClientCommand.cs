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
    /// Успешный результат
    /// </summary>
    public interface IOk
    {
        /// <summary>
        /// Новый идентификатор элемента
        /// </summary>
        int Id { get; set; }
    }

    /// <summary>
    /// Событие о добавлении нового элемента
    /// </summary>
    public interface IClientAddedEvent
    {
        Client Client { get; set; }
    }
}
