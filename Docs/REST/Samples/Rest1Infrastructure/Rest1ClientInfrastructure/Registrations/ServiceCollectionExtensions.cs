using FluentValidation.AspNetCore;
using Rest1ClientInfrastructure.Validators;

namespace Rest1ClientInfrastructure.Registrations;

/// <summary>
/// Регистрация сервисов
/// </summary>
public static class ServiceCollectionExtensions
{
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
