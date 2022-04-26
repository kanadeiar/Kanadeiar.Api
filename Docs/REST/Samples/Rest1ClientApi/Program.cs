var builder = WebApplication.CreateBuilder(args);

builder.Services.MyDatabase(builder.Configuration);
builder.Services.AddTransient<TestData>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.KndAddSwagger("Rest1ClientApi", "v1", "rest1clientapi.xml", new[] { "rest1clientapplication.xml" });
builder.Services.KndAddMapster();
builder.Services.KndAddMediatR(typeof(GetClientByIdHandler).Assembly);
builder.Services.KndAddHealthCheck();
builder.Services.MyAddRepositories();
builder.Services.MyAddFluentValidation();

builder.Host.UseSerilog((host, log) =>
{
    log.ReadFrom.Configuration(host.Configuration)
        .MinimumLevel.Debug()
#if DEBUG
        .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
#else
        .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
#endif
        .WriteTo.RollingFile($@".\Logs\Osinit.Portal.News.Api_[{DateTime.Now:yyyy-MM-dd}].log")
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rest1ClientApi");
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

app.KndSeedTestData<TestData>();

app.MapHealthChecks("/healthz");

app.Run();
