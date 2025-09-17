using System;
using BusinessEntities;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderData : IdObjectData
    {
        public OrderData(Order order) : base(order)
        {
            Name = order.Name;
            Description = order.Description;
            TotalPrice = order.TotalPrice;
            ProductIds = order.ProductIds;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}