namespace MT1ClientInfra.BackgroundServices;

/// <summary>
/// Сервис инициализации базы данных
/// </summary>
public class TestDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    public TestDataBackgroundService(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var seeder = scope.ServiceProvider
                .GetRequiredService<TestData>()
                .SeedTestData(scope.ServiceProvider);
        }
    }
}
