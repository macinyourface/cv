using BusinessEntities;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, ProductTypes type, decimal? price);
    }
}
