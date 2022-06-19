namespace MT1GatewayInfra.Registrations;

public static class ServiceCollectionExtensions
{
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
