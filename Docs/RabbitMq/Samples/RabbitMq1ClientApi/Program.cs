
using RabbitMq1ClientInfrastructure.Registrations;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.MyDatabase(hostContext.Configuration);
        services.AddMassTransit(x => 
        {
            x.AddConsumer<GetClientConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));

            x.UsingRabbitMq((context, config) =>
            {
                config.Host("localhost", "/", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.ReceiveEndpoint("RabbitMq1ClientApi", e => 
                {
                    e.UseInMemoryOutbox();
                    e.Consumer<ClientToCreateConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
                });
                config.ConfigureEndpoints(context);
            });
            //x.AddRequestClient<GetClientConsumer>(new Uri("exchange:order-status"));
        });        
        //services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
