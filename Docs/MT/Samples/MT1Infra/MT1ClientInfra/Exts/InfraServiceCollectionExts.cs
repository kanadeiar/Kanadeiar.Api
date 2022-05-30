namespace MT1ClientInfra.Exts;

/// <summary>
/// Инфраструктурные сервисы
/// </summary>
public static class InfraServiceCollectionExts
{
    /// <summary>
    /// Добавление базы данных
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection MyDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ClientDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetValue<string>("ConnectionString"));
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
        services.AddScoped<DbContext, ClientDbContext>();

        return services;
    }

    /// <summary>
    /// Добавление репозиториев
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection MyAddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepo, ClientRepo>();

        return services;
    }

    /// <summary>
    /// Добавление тестовых данных
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection MyAddTestData(this IServiceCollection services)
    {
        services.AddTransient<TestData>();
        services.AddHostedService<TestDataBackgroundService>();

        return services;
    }
}
