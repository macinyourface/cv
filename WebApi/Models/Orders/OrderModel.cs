using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public List<Guid> ProductIds { get; set; }
    }
}
