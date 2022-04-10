﻿using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Kanadeiar.Api.Registrations;

/// <summary>
/// Регистрация сервисов
/// </summary>
public static class ServiceCollectionExtentions
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
    public static IServiceCollection AddServiceSwagger(this IServiceCollection services, string title, string version = "v1", string? filename = default, params string[] domainFilenames)
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
}
