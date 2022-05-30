namespace gRpc1ClientInfrastructure.Registrations;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection MyAddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IClientRepository, ClientRepository>();

        return services;
    }
}
