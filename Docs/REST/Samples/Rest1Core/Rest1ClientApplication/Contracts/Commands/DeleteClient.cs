namespace Rest1ClientApplication.Contracts.Commands;

/// <summary>
/// Удалить элемент
/// </summary>
public class DeleteClient : IRequest<bool>
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Удалить элемент
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    public DeleteClient(int id)
    {
        Id = id;
    }
}
