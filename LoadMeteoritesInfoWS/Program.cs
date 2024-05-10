using LoadMeteoritesInfoWS;
using Infrastructure;
using Application;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddInfrastructureLayer();
builder.Services.AddApplicationLayer();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
