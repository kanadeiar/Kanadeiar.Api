
using Rest1ClientApplication.Implementations.Queries;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ClientContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetValue<string>("ConnectionString"),
        o => o.MigrationsAssembly("Rest1ClientInfrastructure"));
#if DEBUG
    options.EnableSensitiveDataLogging();
#endif
});
builder.Services.AddScoped<DbContext, ClientContext>();

// TODO : � ���������� - ������� �����������
builder.Services.AddTransient<TestData>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.KndAddSwagger("Rest1ClientApi", "v1", "rest1clientapi.xml", new[] { "rest1clientapplication.xml" });
builder.Services.KndAddMapster();

// TODO : ��� �� �������� - ��� �� ��������������
builder.Services.AddScoped<IClientRepository, ClientRepository>();

// TODO : ��� � ����������
var handlersAssembly = typeof(GetClientByIdHandler).Assembly;
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), handlersAssembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin());

app.MapControllers();

// TODO: � ����������
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider
        .GetRequiredService<TestData>()
        .SeedTestData(scope.ServiceProvider);
}

app.Run();
