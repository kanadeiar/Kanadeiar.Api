namespace Lab1ClientApi.BackgroundServices;

public sealed class TestDataBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TestDataBackgroundService> _logger;
    public TestDataBackgroundService(IServiceProvider serviceProvider, ILogger<TestDataBackgroundService> logger)
        => (_serviceProvider, _logger) = (serviceProvider, logger);

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
