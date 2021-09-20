using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Models;

namespace ECommerce.API.Search.Interfaces
{
    public interface IProductService
    {
        Task<(bool isSuccess, IEnumerable<Product> Products, string Message)> GetProductsAsync();
    }
}
