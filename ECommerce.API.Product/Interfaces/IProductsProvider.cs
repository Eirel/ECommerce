using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.ProductModel> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool isSuccess, Models.ProductModel Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
