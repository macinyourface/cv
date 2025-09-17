using System;
using System.Collections.Generic;
using BusinessEntities;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order, string name, string description, decimal? totalPrice, List<Guid> products);
    }
}
