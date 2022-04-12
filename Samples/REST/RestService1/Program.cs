var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.KanadeiarAddSwagger("WebApiTest1", filename: "info.xml", domainFilenames: new[] { "sample.application.xml" });
builder.Services.KanadeiarAddMapster();

builder.Services.AddScoped<GenPersonService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
