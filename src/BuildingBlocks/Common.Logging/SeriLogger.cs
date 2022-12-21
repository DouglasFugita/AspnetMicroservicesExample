using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace Common.Logging;
public static class SeriLogger
{
    public static Action<HostBuilderContext, LoggerConfiguration>
        Configure => (ctx, loggerConfiguration) =>
        {
            var elasticUri = ctx.Configuration.GetValue<string>("ElasticConfiguration:Uri");
            var elasticSinkConfig = new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                IndexFormat = $"applogs-{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{ctx.HostingEnvironment.EnvironmentName?.ToLower().Replace(".", "-")}-logs-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                NumberOfShards = 2,
                NumberOfReplicas = 1
            };

            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.Elasticsearch(elasticSinkConfig)
                .ReadFrom.Configuration(ctx.Configuration);
        };

}
