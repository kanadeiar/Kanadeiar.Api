IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.MyDatabase(hostContext.Configuration);
        services.MyAddRepositories();
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetClientByIdQueryHandler).Assembly);

        services.MyMassTransit();

        services.MyAddTestData();
    })
    .Build();

await host.RunAsync();
