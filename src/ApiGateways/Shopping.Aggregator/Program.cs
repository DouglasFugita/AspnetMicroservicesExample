using Common.Logging;
using Common.Resilience;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Polly;
using Serilog;
using Shopping.Aggregator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<LoggingDelegatingHandler>();

builder.Services.AddHttpClient<ICatalogService, CatalogService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:CatalogUrl")))
    .AddHttpMessageHandler<LoggingDelegatingHandler>();

builder.Services.AddHttpClient<IBasketService, BasketService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:BasketUrl")))
    .AddHttpMessageHandler<LoggingDelegatingHandler>()
    .AddPolicyWrapperAsyncHandler();

builder.Services.AddHttpClient<IOrderService, OrderService>(c =>
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiSettings:OrderingUrl")))
    .AddHttpMessageHandler<LoggingDelegatingHandler>();

builder.Services.AddHealthChecks()
    .AddUrlGroup(new Uri($"{builder.Configuration.GetValue<string>("ApiSettings:CatalogUrl")}/swagger/index.html"), "Catalog API", HealthStatus.Unhealthy)
    .AddUrlGroup(new Uri($"{builder.Configuration.GetValue<string>("ApiSettings:BasketUrl")}/swagger/index.html"), "Basket API", HealthStatus.Unhealthy)
    .AddUrlGroup(new Uri($"{builder.Configuration.GetValue<string>("ApiSettings:OrderingUrl")}/swagger/index.html"), "Ordering API", HealthStatus.Unhealthy);

builder.Host.UseSerilog(SeriLogger.Configure);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
