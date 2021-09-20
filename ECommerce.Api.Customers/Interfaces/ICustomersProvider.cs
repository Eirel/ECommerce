using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync();
        Task<(bool isSuccess, Models.CustomerModel Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
