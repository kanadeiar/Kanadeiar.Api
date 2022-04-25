using Kanadeiar.Api.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kanadeiar.Api.Registrations;

/// <summary>
/// Заполнение базы данных тестовыми данными
/// </summary>
public static class ApplicationBuilderExtentions
{
    /// <summary>
    /// Заполнить базу данных тестовыми данными
    /// </summary>
    /// <typeparam name="T">Тип сервиса заполнения</typeparam>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder KndSeedTestData<T>(this IApplicationBuilder app)
        where T : IKndTestData
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var seeder = scope.ServiceProvider
                .GetRequiredService<T>()
                .SeedTestData(scope.ServiceProvider);
        }

        return app;
    }
}
