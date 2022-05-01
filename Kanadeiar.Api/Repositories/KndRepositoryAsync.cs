using Kanadeiar.Core.Domain.Base;
using Kanadeiar.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Kanadeiar.Api.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T"></typeparam>
public class KndRepositoryAsync<T, TId> : IKndRepositoryAsync<T, TId> where T : class, IKndEntity<TId>
{
    private readonly DbContext _context;
    public KndRepositoryAsync(DbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Query => _context.Set<T>();

    public async IAsyncEnumerable<T> GetPagedAsync(int offset, int count, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var item in _context
            .Set<T>()
            .OrderBy(_ => _.Id)
            .Skip(offset)
            .Take(count)
            .AsNoTracking())
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;
            yield return item;
        }
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        T exist = await _context.Set<T>().FindAsync(new object[] { entity.Id }, cancellationToken);
        _context.Entry(exist).CurrentValues.SetValues(entity);
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
