using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kanadeiar.Api.Interfaces;

/// <summary>
/// Заполнение базы данных тестовыми данными
/// </summary>
public interface IKndTestData
{
    /// <summary>
    /// Заполнить базу данных тестовыми данными
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    Task SeedTestData(IServiceProvider provider);
}
