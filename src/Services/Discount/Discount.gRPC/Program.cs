using Discount.gRPC.Repositories;
using Discount.Grpc.Services;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();

    try
    {

        using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        connection.Open();

        using var command = new NpgsqlCommand
        {
            Connection = connection
        };

        command.CommandText = "DROP TABLE IF EXISTS Coupon";
        command.ExecuteNonQuery();

        command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
        command.ExecuteNonQuery();

        command.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 20', 'Samsung Discount', 100);";
        command.ExecuteNonQuery();

    }
    catch (Exception)
    {

    }

}

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
