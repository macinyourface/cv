using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using Common;
using Raven.Client;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _documentSession;

        // Local static collection to replace database
        private static Dictionary<Guid,Product> _productRepository = new Dictionary<Guid,Product>();

        public ProductRepository(IDocumentSession documentSession) : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public new Product Get(Guid productId)
        {
            Product product = null;

            if (_productRepository.ContainsKey(productId))
            {
                product = _productRepository[productId];
            }
            return product;
        }

        public IEnumerable<Product> Get(string name = null, ProductTypes? type = null)
        {
            List<Product> products = new List<Product>();

            var hasFirstParameter = false;
            if(name != null)
            {
                products = _productRepository.Where(x => x.Value.Name == name).Select(y => y.Value).ToList();
                hasFirstParameter = true;
            } else
            {
                products = _productRepository.Select(x => x.Value).ToList();
            }

            if (type != null) 
            {
                if (hasFirstParameter)
                {
                    products = products.Where(x => x.Type == type).ToList();
                } else
                {
                    products = _productRepository.Where(x => x.Value.Type == type).Select(y => y.Value).ToList();   
                }
            }

            return products;
        }

        public void DeleteAll()
        {
            _productRepository.Clear();
        }

        public new void Delete(Product product)
        {
            _productRepository.Remove(product.Id);
        }

        public new void Save(Product product)
        {
            _productRepository.Add(product.Id, product);
        }
    }
}
