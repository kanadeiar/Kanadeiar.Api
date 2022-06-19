IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.MyDatabase(hostContext.Configuration);
        services.MyAddRepositories();
        services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(GetClientByIdQueryHandler).Assembly);

        services.MyMassTransit();

        services.MyAddTestData();
    })
    .UseSerilog((host, log) =>
    {
        log.ReadFrom.Configuration(host.Configuration)
            .MinimumLevel.Debug()
#if DEBUG
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
#else
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
#endif
            .WriteTo.RollingFile($@".\Logs\MT1ClientyApi_[{DateTime.Now:yyyy-MM-dd}].log")
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}");
    })
    .Build();

await host.RunAsync();
