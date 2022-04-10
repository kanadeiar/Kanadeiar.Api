using Kanadeiar.Api;
using Kanadeiar.Api.Registrations;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceSwagger("WebApiTest1");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
