using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository: IDiscountRepository
{
    private readonly IConfiguration _configuration;


    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration?? throw new ArgumentNullException(nameof(configuration));
    }

    public Task<bool> CreateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteDiscount(string productName)
    {
        throw new NotImplementedException();
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM Coupon WHERE ProductName = @ProductName", new {ProductName = productName});

        if (coupon == null)
        {
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount" };
        }

        return coupon;
    }

    public Task<bool> UpdateDiscount(Coupon coupon)
    {
        throw new NotImplementedException();
    }
}
