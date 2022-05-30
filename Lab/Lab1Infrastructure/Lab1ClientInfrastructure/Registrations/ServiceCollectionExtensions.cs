namespace Lab1ClientInfrastructure.Registrations;

public static class ServiceCollectionExtensions
{
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

    public static IServiceCollection MyAddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}
