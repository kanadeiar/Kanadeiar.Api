namespace MT1ClientApi.Exts;

/// <summary>
/// Сервисы приложения
/// </summary>
public static class ServiceCollectionExts
{
    /// <summary>
    /// Добавление шины данных MassTransit
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <returns>Сервисы</returns>
    public static IServiceCollection MyMassTransit(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<GetPagedClientQueryConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
            x.AddConsumer<GetClientCountQueryConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));

            //x.AddConsumer<ClientCommandConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
            x.UsingRabbitMq((context, config) =>
            {
                config.Host("localhost", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.ReceiveEndpoint("MT1ClientApi", e =>
                {
                    e.UseInMemoryOutbox();
                    //e.ConfigureConsumer<ClientCommandConsumer>(context);
                });
                config.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
