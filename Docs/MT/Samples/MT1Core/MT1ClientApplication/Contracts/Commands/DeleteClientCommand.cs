namespace MT1ClientApplication.Contracts.Commands;

/// <summary>
/// Удаление элемента
/// </summary>
public class DeleteClientCommand : IRequest<bool>
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Удаление элемента
    /// </summary>
    /// <param name="id"></param>
    public DeleteClientCommand(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Уведомление о удалении элемента
    /// </summary>
    public interface IClientDeleted
    {
        public int Id { get; set; }
    }
}
