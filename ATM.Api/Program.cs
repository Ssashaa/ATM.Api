using ATM.Api.Helpers;
using ATM.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());

app.Run();