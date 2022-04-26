namespace Rest1ClientApplication.Contracts.Queries;

/// <summary>
/// Получение клиентов со смещением и количеством
/// </summary>
public class GetPagedClient : IStreamRequest<ClientDto>
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
    public GetPagedClient(int offset, int count)
    {
        Offset = offset;
        Count = count;
    }
}
