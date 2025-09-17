using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateOrderService : IUpdateOrderService
    {
        public void Update(Order order, string name, string description, decimal? price, List<Guid> products)
        {
            order.SetName(name);
            order.SetPrice(price.Value);
            order.SetDescription(description);
            order.SetProducts(products);
        }
    }
}