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
    /// Успешный результат
    /// </summary>
    public interface IOk
    {
        /// <summary>
        /// Успешность
        /// </summary>
        bool Success { get; set; }
    }

    /// <summary>
    /// Событие о удалении элемента
    /// </summary>
    public interface IClientDeletedEvent
    {
        int Id { get; set; }
    }
}
