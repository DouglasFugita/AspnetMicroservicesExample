using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",true,true).Build();
builder.Services.AddOcelot(configuration);


var app = builder.Build();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
