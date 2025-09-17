using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Products;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductController : BaseApiController
    {
        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductController(ICreateProductService createProductService, 
                                 IDeleteProductService deleteProductService, 
                                 IGetProductService getProductService, 
                                 IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid productId, [FromBody] ProductModel productModel)
        {
            if (_getProductService.GetProduct(productId) == null)
            {
                var newProduct = _createProductService.Create(productId, productModel.Name, productModel.Type, productModel.Price);
                return Found(new ProductData(newProduct));
            }
            else
            {
                return AlreadyExists("A product already exists with that specific product Id: " + productId);
            }
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUser(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.GetProduct(productId);

            if (product == null)
            {
                return DoesNotExist();
            }

            // Check to make sure the Name and Email strings are not null or empty
            if (string.IsNullOrEmpty(model.Name) || model.Price == null)
            {
                return ValueCannotBeNull("Check that Name or Price are not null or empty");
            }

            _updateProductService.Update(product, model.Name, model.Type, model.Price);

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }
            _deleteProductService.Delete(product);

            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetProduct(Guid productId)
        {
            var product = _getProductService.GetProduct(productId);
            if (product == null)
            {
                return DoesNotExist();
            }

            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        //public HttpResponseMessage GetProducts(int skip, int take, ProductTypes? type = null, string name = null)
        public HttpResponseMessage GetProducts(ProductTypes? type = null, string name = null)
        {
            var products = _getProductService.GetProducts(name, type)
                           .Select(q => new ProductData(q))
                           .ToList();

            return Found(products);
        }

    }
}