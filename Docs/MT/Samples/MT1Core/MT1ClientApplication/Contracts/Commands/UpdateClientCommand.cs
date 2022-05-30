namespace MT1ClientApplication.Contracts.Commands;

/// <summary>
/// Обновление элемента
/// </summary>
public class UpdateClientCommand : IRequest<bool>
{
    /// <summary>
    /// Идентификатор обновляемого
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Новые данные
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// Обновление элемента
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="client">Новые данные</param>
    public UpdateClientCommand(int id, Client client)
    {
        Id = id;
        Client = client;
    }

    /// <summary>
    /// Уведомление о обновлении элемента
    /// </summary>
    public interface IClientUpdated
    {
        public int Id { get; set; }
        Client Client { get; set; }
    }
}
