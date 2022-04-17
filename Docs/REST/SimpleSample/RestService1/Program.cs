using Kanadeiar.Api.Registrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();

builder.Services.KarAddSwagger("WebApiTest1");
builder.Services.KarAddMapster();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(builder => builder.AllowAnyOrigin());

app.MapControllers();

app.Run();
