using BusinessEntities;

namespace WebApi.Models.Products
{
    public class ProductData : IdObjectData
    {
        public ProductData(Product product) : base(product)
        {
            Name = product.Name;
            Type = new EnumData(product.Type);
            Price = product.Price;
        }

        public string Name { get; set; }
        public EnumData Type { get; set; }
        public decimal? Price { get; set; }
    }
}