using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<(bool isSuccess, Customer Customer, string Message)> GetCustomerAsync(int id);
    }
}
