using Common.Logging;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true).Build();
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"ocelot.json").Build();
builder.Services.AddOcelot(configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
    ;

builder.Host.UseSerilog(SeriLogger.Configure);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
