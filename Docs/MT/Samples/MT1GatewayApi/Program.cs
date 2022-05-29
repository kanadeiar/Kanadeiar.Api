var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();

builder.Services.KndAddSwagger("MT1Gateway", "v1");
builder.Services.KndAddMapster();


builder.Services.AddMassTransit(x =>
{

    x.UsingRabbitMq((context, config) => 
    {
        config.Host("localhost", h => {
            h.Username("guest");
            h.Password("guest");
        });
        config.ReceiveEndpoint("MT1Gateway", e =>
        {
            e.UseInMemoryOutbox();
        });
        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "MT1Gateway");
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

app.Run();
