using AspnetRunBasics6.Models;

namespace AspnetRunBasics6.Services;

public interface IBasketService
{
    Task<BasketModel> GetBasket(string userName);
    Task<BasketModel> UpdateBasket(BasketModel model);
    Task CheckoutBasket(BasketCheckoutModel model);
}
