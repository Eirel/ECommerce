using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Db.Order, Models.OrderModel>();
            CreateMap<Db.OrderItem, Models.OrderItemModel>();
        }
    }
}
