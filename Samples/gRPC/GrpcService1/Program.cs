using GrpcService1.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<PersonsService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
