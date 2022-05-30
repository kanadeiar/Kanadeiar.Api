namespace MT1ClientApplication.Contracts.Queries;

/// <summary>
/// Получение элементов с постраничной разбивкой
/// </summary>
public class GetPagedClientQuery : IRequest<IEnumerable<Client>>
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
    /// Получение количества элементов
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    public GetPagedClientQuery(int offset, int count)
    {
        Offset = offset;
        Count = count;
    }

    /// <summary>
    /// Успешный результат
    /// </summary>
    public interface IOk
    {
        IEnumerable<Client> Clients { get; set; }
    }
}
