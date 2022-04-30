namespace Rest1ClientApplication.Contracts.Queries;

/// <summary>
/// Контракт получения одного элемента
/// </summary>
public class GetClientByIdQuery : IRequest<ClientDto?>
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Получение одного элемента
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    public GetClientByIdQuery(int id)
    {
        Id = id;
    }
}
