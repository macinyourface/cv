using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        // Local static collection to replace database
        private static Dictionary<Guid, Order> _orderRepository = new Dictionary<Guid, Order>();

        public OrderRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public new Order Get(Guid orderId)
        {
            Order order = null;

            if (_orderRepository.ContainsKey(orderId))
            {
                order = _orderRepository[orderId];
            }
            return order;
        }

        public IEnumerable<Order> Get(string name = null)
        {
            List<Order> orders = new List<Order>();

            if (name != null)
            {
                orders = _orderRepository.Where(x => x.Value.Name == name).Select(y => y.Value).ToList();
            }
            else
            {
                orders = _orderRepository.Select(x => x.Value).ToList();
            }

            return orders;
        }

        public void DeleteAll()
        {
            _orderRepository.Clear();
        }

        public new void Delete(Order order)
        {
            _orderRepository.Remove(order.Id);
        }

        public new void Save(Order order)
        {
            _orderRepository.Add(order.Id, order);
        }
    }
}
