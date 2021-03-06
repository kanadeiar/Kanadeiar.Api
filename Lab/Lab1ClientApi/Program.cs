IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.MyDatabase(hostContext.Configuration);
        services.MyAddRepositories();
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetClientByIdQueryHandler).Assembly);

        services.AddMassTransit(x => 
        {
            x.AddConsumer<ClientQueryConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
            x.AddConsumer<ClientCommandConsumer>(c => c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));

            x.UsingRabbitMq((context, config) => 
            {
                config.Host("localhost", h => {
                    h.Username("guest");
                    h.Password("guest");
                });
                config.ReceiveEndpoint("Lab1ClientApi", e =>
                {
                    e.UseInMemoryOutbox();
                    e.ConfigureConsumer<ClientCommandConsumer>(context);
                });

                config.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<TestData>();
        services.AddHostedService<TestDataBackgroundService>();
    })
    .Build();

await host.RunAsync();
