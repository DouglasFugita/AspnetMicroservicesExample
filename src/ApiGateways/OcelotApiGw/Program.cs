var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
