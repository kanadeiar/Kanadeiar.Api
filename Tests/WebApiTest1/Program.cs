using Kanadeiar.Api;
using Kanadeiar.Api.Registrations;
using Microsoft.AspNetCore.Builder;
using WebApiTest1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddServiceSwagger("WebApiTest1", filename:"info.xml");

builder.Services.AddScoped<PersonService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
