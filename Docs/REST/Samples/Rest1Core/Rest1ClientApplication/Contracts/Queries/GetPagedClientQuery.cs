namespace Rest1ClientApplication.Contracts.Queries;

/// <summary>
/// Получение клиентов со смещением и количеством
/// </summary>
public class GetPagedClientQuery : IStreamRequest<ClientDto>
{
    /// <summary>
    /// Смещение
    /// </summary>
    public int Offset { get; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Получение клиентов со сменением и количеством
    /// </summary>
    public GetPagedClientQuery(int offset, int count)
    {
        Offset = offset;
        Count = count;
    }
}
