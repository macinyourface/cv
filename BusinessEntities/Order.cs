using System;
using System.Collections.Generic;
using Common.Extensions;
using System.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private List<Guid> _listOfProductIds = new List<Guid>();
        private string _name;
        private string _description;
        private decimal _totalPrice;

        public String Name
        {
            get => _name;
            private set => _name = value;
        }

        public String Description
        {
            get => _description;
            private set => _description = value;
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            private set => _totalPrice = value;
        }

        public List<Guid> ProductIds
        {
            get => _listOfProductIds;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Name was not provided");
            }
            _name = name;
        }

        public void SetPrice(decimal price)
        {
            _totalPrice = price;
        }

        public void SetDescription(string description)
        {
            _description = description;
        }

        public void AddProductId(Guid productId)
        {
            _listOfProductIds.Add(productId);
        }

        public void SetProducts(List<Guid> products)
        {
            _listOfProductIds = products;  // Shallow copy but just go with it for now
        }
    }
}
