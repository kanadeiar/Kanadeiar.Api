using Kanadeiar.Api;
using Kanadeiar.Api.Registrations;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using WebApiTest1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.KanadeiarAddSwagger("WebApiTest1", filename:"info.xml");

builder.Services.AddGrpc();

builder.Services.AddScoped<PersonService>();



var config = TypeAdapterConfig.GlobalSettings;
config.Scan(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<IMapper, ServiceMapper>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapGrpcService<PersonInfoService>();

app.Run();
