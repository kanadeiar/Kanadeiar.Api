using Rest1Core.Entities;

namespace Rest1ClientApi;

/// <summary>
/// Заполнение тестовыми данными
/// </summary>
public class TestData
{
    private static class Data
    {
        public async static Task PopulateTestData(DbContext context, ILogger<TestData> logger)
        {
            logger.LogInformation("Начало заполнения тестовыми данными ...");

            var clients = Enumerable.Range(1, 30).Select(x => new Client
            {
                UserId = x,
                LastName = $"Иванов_{x}",
                FirstName = $"Иван_{x}",
                Patronymic = $"Иванович_{x}",
                BirthDay = DateTime.Today.AddYears(-20).AddDays(x),
            });
            context.Set<Client>().AddRange(clients);
            await context.SaveChangesAsync();

            logger.LogInformation("Конец заполнения тестовыми данными");
        }
    }

    /// <summary>
    /// Заполнение тестовыми данными
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task SeedTestData(IServiceProvider provider)
    {
        var logger = provider.GetRequiredService<ILogger<TestData>>();
        using var context = new ClientContext(provider.GetRequiredService<DbContextOptions<ClientContext>>());
        
        if (context == null || context.Set<Client>() == null)
        {
            logger.LogError("Null ClientContext");
            throw new ArgumentNullException("Null ClientContext");
        }
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Применение миграций: {string.Join(",", pendingMigrations)}");
            await context.Database.MigrateAsync();
        }
        if (context.Clients.Any())
        {
            logger.LogInformation("База данных уже содержит данные - заполнение данными пропущено");
            return;
        }

        await Data.PopulateTestData(context, logger);
    }
}
