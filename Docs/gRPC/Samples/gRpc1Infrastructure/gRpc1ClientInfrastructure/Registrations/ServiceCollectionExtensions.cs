namespace gRpc1ClientInfrastructure.Registrations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection MyAddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();

        return services;
    }
}
