using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _cache;

    public BasketRepository(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<ShoppingCart?> GetBasket(string userName)
    {
        var basket = await _cache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await _cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize<ShoppingCart>(basket));
        return await GetBasket(basket.UserName);
    }

    public async Task DeleteBasket(string userName)
    {
        await _cache.RemoveAsync(userName);
    }

}
