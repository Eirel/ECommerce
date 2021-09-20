using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IOrderService
    {
        Task<(bool isSuccess, IEnumerable<Order> Orders, String Message)> GetOrderAsync(int customerId);
    }
}
