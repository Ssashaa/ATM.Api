using ATM.Api;
using ATM.Api.Service.Interfaces;
using ATM.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app
    .UseRouting()
    .UseSwagger()
    .UseSwaggerUI()
    .UseEndpoints(x => x.MapControllers());

app.Run();