namespace Lab1ClientApi;

public class TestData : IKndTestData
{
    private class InternalData
    {
        private Random _rnd = new Random();

        public async Task PopulateTestData(DbContext context, ILogger<TestData> logger)
        {
            logger.LogInformation("Начало заполнения тестовыми данными ...");

            var clients = Enumerable.Range(1, 100).Select(x => new Client
            {
                UserId = _rnd.Next(1, 10),
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

    public async Task SeedTestData(IServiceProvider provider)
    {
        var logger = provider.GetRequiredService<ILogger<TestData>>();
        using var context = new ClientDbContext(provider.GetRequiredService<DbContextOptions<ClientDbContext>>());

        if (context == null || context.Set<Client>() == null)
        {
            logger.LogError("Контекст базы данных ClientContext = null");
            throw new ArgumentNullException("Контекст базы данных ClientContext = null");
        }
        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            logger.LogInformation($"Применение миграций: {string.Join(",", pendingMigrations)}");
            await context.Database.MigrateAsync();
        }
        if (context.Set<Client>().Any())
        {
            logger.LogInformation("База данных уже содержит данные - заполнение данными пропущено");
            return;
        }

        await new InternalData().PopulateTestData(context, logger);
    }
}
