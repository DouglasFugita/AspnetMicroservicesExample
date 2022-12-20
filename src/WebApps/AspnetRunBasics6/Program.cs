using AspnetRunBasics6.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:GatewayAddress")));
builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:GatewayAddress")));
builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:GatewayAddress")));

builder.Host.UseSerilog((ctx, lc) =>
    lc
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(
            new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(
                new Uri(builder.Configuration.GetValue<string>("ElasticConfiguration:Uri")))
            {
                IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{ctx.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1

            })
        .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
        .ReadFrom.Configuration(ctx.Configuration)
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
