namespace MT1ClientApplication.Contracts.Queries;

/// <summary>
/// Получение количества элементов
/// </summary>
public class GetClientCountQuery : IRequest<int>
{
    /// <summary>
    /// Получение одного элемента
    /// </summary>
    public GetClientCountQuery()
    { }

    /// <summary>
    /// Успешное получение количества элементов
    /// </summary>
    public interface IOk
    {
        int Count { get; set; }
    }
}
