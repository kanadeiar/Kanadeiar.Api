namespace Rest1ClientApplication.Contracts;

/// <summary>
/// Контракт получения одного элемента
/// </summary>
public class GetClientById : IRequest<ClientDto?>
{
    /// <summary>
    /// Идентификатор элемента
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Получение одного элемента
    /// </summary>
    /// <param name="id">Идентификатор элемента</param>
    public GetClientById(int id)
    {
        Id = id;
    }
}
