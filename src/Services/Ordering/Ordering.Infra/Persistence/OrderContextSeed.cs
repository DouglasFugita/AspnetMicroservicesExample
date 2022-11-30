using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infra.Persistence;
public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order()
            {
                UserName = "usuario",
                FirstName = "Doug",
                LastName = "Fugita", 
                EmailAddress = "usuario@email.com", 
                AddressLine = "Endereco", 
                Country = "Brazil", 
                TotalPrice = 350
            }
        };
    }
}
