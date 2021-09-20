using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Orders.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total
        {
            get
            {
                if (Items?.Any() ?? false)
                {
                    return Items.Sum(i => i.Quantity * i.UnitPrice);
                }
                return 0;
            }
        }

        public List<OrderItemModel> Items { get; set; }
    }
}
