var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.KndAddSwagger("MT1Gateway", "v1", domainFilenames: new[] { "mt1gateway.xml" });
builder.Services.KndAddMapster();
builder.Services.MyAddFluentValidation();

builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, config) => 
    {
        config.Host("localhost", h => {
            h.Username("guest");
            h.Password("guest");
        });
        config.ReceiveEndpoint("MT1Gateway", e =>
        {
            e.UseInMemoryOutbox();
        });
        config.ConfigureEndpoints(context);
    });
});

builder.Host.UseSerilog((host, log) =>
{
    log.ReadFrom.Configuration(host.Configuration)
        .MinimumLevel.Debug()
#if DEBUG
        .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
#else
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
#endif
        .WriteTo.RollingFile($@".\Logs\MT1GatewayApi_[{DateTime.Now:yyyy-MM-dd}].log")
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MT1Gateway");
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
