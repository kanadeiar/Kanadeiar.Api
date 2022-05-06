var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.KndAddMediatR(typeof(GetClientByIdQueryHandler).Assembly);
builder.Services.MyAddRepositories();

var app = builder.Build();

app.MapGrpcService<ClientGrpcService>();

app.Run();
