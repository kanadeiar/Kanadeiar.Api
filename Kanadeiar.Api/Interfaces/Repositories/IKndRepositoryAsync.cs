using Kanadeiar.Core.Domain.Base;

namespace Kanadeiar.Api.Interfaces.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T">сущность</typeparam>
/// <typeparam name="TId">тип ключа сущности</typeparam>
public interface IKndRepositoryAsync<T, in TId> where T : class, IKndEntity<TId>
{
    /// <summary>
    /// Запрос
    /// </summary>
    IQueryable<T> Query { get; }

    /// <summary>
    /// Асинхронная коллекция всех сущностей с пагинацией
    /// </summary>
    /// <param name="offset">количество пропускаемых элементов</param>
    /// <param name="count">количество захватываемых</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Асинхронная коллекция</returns>
    IAsyncEnumerable<T> GetPagedAsync(int offset, int count, CancellationToken cancellationToken);

    /// <summary>
    /// Обычная коллекция всех сущностей с пагинацией
    /// </summary>
    /// <param name="offset">количество элементов пропускаемых</param>
    /// <param name="count">количество захватываемых</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Обычная коллекция</returns>
    Task<IEnumerable<T>> GetPaged(int offset, int count, CancellationToken cancellationToken);

    /// <summary>
    /// Получить один элемент
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить элемент
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить элемент
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

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
