using Kanadeiar.Api.Domain.Base;
using Kanadeiar.Api.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Kanadeiar.Api.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="T"></typeparam>
public class KndRepositoryAsync<T, TId> : IKndRepositoryAsync<T, TId> where T : KndEntity<TId>
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
            .OrderByDescending(_ => _.Id)
            .Skip(offset)
            .Take(count)
            .AsNoTracking())
        {
            if (cancellationToken.IsCancellationRequested)
                yield break;
            yield return item;
        }
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        T exist = await _context.Set<T>().FindAsync(entity.Id);
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
