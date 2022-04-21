namespace Rest1ClientApplication.Interfaces.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepositoryAsync<T> where T : class, IEntity
{
    /// <summary>
    /// Запрос
    /// </summary>
    IQueryable<T> Query { get; }

    /// <summary>
    /// Все сущности с пагинацией
    /// </summary>
    /// <param name="lastId">послдений идентификатор захватываемый</param>
    /// <param name="count">количество захватываемых</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    IAsyncEnumerable<T> GetPagedAsync(int lastId, int count, CancellationToken cancellationToken);

    /// <summary>
    /// Получить один элемент
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Добавить элемент
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> AddAsync(T entity);

    /// <summary>
    /// Обновить элемент
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity);

    /// <summary>
    /// Удалить элемент
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task DeleteAsync(T entity);

    /// <summary>
    /// Сохранение изменений в базе данных
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CommitAsync(CancellationToken cancellationToken);
}
