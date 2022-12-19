using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

//IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true).Build();
IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"ocelot.json").Build();
builder.Services.AddOcelot(configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
    ;


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseOcelot().Wait();

app.Run();
