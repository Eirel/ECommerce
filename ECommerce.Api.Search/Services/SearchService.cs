using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.API.Search.Interfaces;

namespace ECommerce.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _oService;
        private readonly IProductService _pService;
        private readonly ICustomerService _cService;

        public SearchService(IOrderService oService, IProductService pService, ICustomerService cService)
        {
            _oService = oService;
            _pService = pService;
            _cService = cService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var oResults = await _oService.GetOrderAsync(customerId);
            var pResults = await _pService.GetProductsAsync();

            if (oResults.isSuccess) {
                
                foreach (var order in oResults.Orders) {
                    foreach (var item in order.Items) {
                        item.ProductName = pResults.isSuccess ?
                            pResults.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name ?? string.Empty
                            : "Product information is not available";
                    }

                    var cResult = await _cService.GetCustomerAsync(customerId);
                    order.Customer = cResult.isSuccess ? cResult.Customer : null; 
                }
                

                var result = new
                {
                    Orders = oResults.Orders
                };

                return (true, result);
            }
            return (false, null);
        }
    }
}
