
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TreasureMap.Interfaces;
using TreasureMap.ObjectCreators;
using TreasureMap.Services;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IImportDataService, ImportDataService>();
//builder.Services.AddScoped<IMovementCreator, SimpleAdvenceMovementCreator>();
//builder.Services.AddScoped<IMovementCreator, SimpleRightMovementCreator>();
//builder.Services.AddScoped<IMovementCreator, SimpleLeftMovementCreator>();
builder.Services.AddSingleton<ITreasureMapService,TreasureMapService >();

IHost host = builder.Build();
host.Services.GetRequiredService<ITreasureMapService>().Play();
await host.RunAsync();
