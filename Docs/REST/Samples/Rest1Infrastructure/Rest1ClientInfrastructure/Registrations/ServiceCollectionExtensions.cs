namespace Rest1ClientInfrastructure.Registrations;

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
        services.AddDbContext<ClientContext>(options =>
        {
            options.UseSqlServer(configuration.GetValue<string>("ConnectionString"),
                o => o.MigrationsAssembly("Rest1ClientInfrastructure")); //можно без этого
#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
        services.AddScoped<DbContext, ClientContext>();

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

    /// <summary>
    /// Регистрация валидации
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection MyAddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidation();
        services.AddTransient<IValidator<ClientDto>, ClientDtoValidator>();

        return services;
    }
}
