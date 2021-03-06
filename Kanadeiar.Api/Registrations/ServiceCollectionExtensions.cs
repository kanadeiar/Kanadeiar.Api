using Kanadeiar.Api.Tools;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Kanadeiar.Api.Registrations;

/// <summary>
/// Регистрация сервисов
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Добавить использование API-сервиса UI Swagger вместе со сгенерированной документации
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="title">Заголовок сваггера</param>
    /// <param name="version">версия</param>
    /// <param name="filename">главный xml файл</param>
    /// <param name="domainFilenames">дополнительные xml файлы</param>
    /// <returns></returns>
    public static IServiceCollection KndAddSwagger(this IServiceCollection services, string title, string version = "v1", string? filename = default, params string[] domainFilenames)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo { Title = title, Version = version });
            if (filename != null)
            {
                options.IncludeXmlComments(filename);
            }
            foreach (var item in domainFilenames)
            {
                AddXmlFileToSwagger(options, item);
            }
        });

        return services;

        static void AddXmlFileToSwagger(SwaggerGenOptions options, string xmlFileName)
        {
            if (File.Exists(xmlFileName))
            {
                options.IncludeXmlComments(xmlFileName);
            }
            else
            {
                options.IncludeXmlComments(Path.Combine("bin/debug/net6.0", xmlFileName));
            }
        }
    }

    /// <summary>
    /// Добавить использование сервиса преобразования данных Mapster
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <returns>Сервисы</returns>
    public static IServiceCollection KndAddMapster(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }

    /// <summary>
    /// Регистрация медиатора MediatR в сервисах приложения
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="mediatrAssembly">Сборка, в которой расположены обработчики и контракты</param>
    /// <returns>сервисы</returns>
    public static IServiceCollection KndAddMediatR(this IServiceCollection services, Assembly mediatrAssembly)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly(), mediatrAssembly);
        return services;
    }

    /// <summary>
    /// Регистрация сервиса проверки работоспособности
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection KndAddHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<ServiceHealthCheck>("Alive");

        return services;
    }
}
