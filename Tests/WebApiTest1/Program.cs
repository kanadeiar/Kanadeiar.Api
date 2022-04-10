using Kanadeiar.Api;

var builder = WebApplication.CreateBuilder(args);

Kanadeiar.Api.Help.Start();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");



app.Run();
