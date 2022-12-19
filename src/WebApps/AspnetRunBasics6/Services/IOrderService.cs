using AspnetRunBasics6.Models;

namespace AspnetRunBasics6.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
}
