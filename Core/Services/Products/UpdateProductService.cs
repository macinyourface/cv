using BusinessEntities;
using Common;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, ProductTypes type, decimal? price)
        {
            product.SetName(name);
            product.SetType(type);
            product.SetPrice(price.Value);
        }
    }
}