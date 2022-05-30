namespace MT1ClientApplication.Contracts.Queries;

/// <summary>
/// Получение одного элемента
/// </summary>
public class GetClientByIdQuery : IRequest<Client?>
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Получение одного элемента
    /// </summary>
    /// <param name="id">Идентификатор</param>
    public GetClientByIdQuery(int id)
    {
        Id = id;
    }

    /// <summary>
    /// Успешный результат
    /// </summary>
    public interface IOk
    {
        /// <summary>
        /// Элемент
        /// </summary>
        Client Client { get; set; }
    }

    /// <summary>
    /// Элемент не найден
    /// </summary>
    public interface INotFound
    {
    }
}
