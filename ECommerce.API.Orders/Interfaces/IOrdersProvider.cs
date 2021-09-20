using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.OrderModel> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool isSuccess, IEnumerable<Models.OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
