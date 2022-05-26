namespace RabbitMq1ClientInfrastructure.Registrations;

/// <summary>
/// Регистрация сервисов
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация базы данных
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
    /// Регистрация репозиториев
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection MyAddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}
