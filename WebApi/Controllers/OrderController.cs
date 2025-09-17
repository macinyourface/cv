using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Orders;
using Core.Services.Products;
using WebApi.Models.Orders;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("orders")]
    public class OrderController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IGetProductService _getProductService;

        public OrderController(ICreateOrderService createOrderService,
                                 IDeleteOrderService deleteOrderService,
                                 IGetOrderService getOrderService,
                                 IUpdateOrderService updateOrderService, 
                                 IGetProductService getProductService)
        {
            _createOrderService = createOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
            _getProductService = getProductService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateProduct(Guid orderId, [FromBody] OrderModel model)
        {
            if (_getOrderService.GetOrder(orderId) == null)
            { 
                // Check if the product exists in the product database
                foreach(Guid mProduct in model.ProductIds)
                {
                    if (_getProductService.GetProduct(mProduct) == null)
                    {
                        return DoesNotExist();
                    }
                }

                var newOrder = _createOrderService.Create(orderId, model.Name, model.Description, model.Price, model.ProductIds);
                return Found(new OrderData(newOrder));
            }
            else
            {
                return AlreadyExists("An order already exists with that specific order Id: " + orderId);
            }
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateOrder(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.GetOrder(orderId);

            if (order == null)
            {
                return DoesNotExist();
            }

            // Check to make sure the Name and Email strings are not null or empty
            if (string.IsNullOrEmpty(model.Name) || model.Price == null || 
                model.ProductIds == null || model.ProductIds.Count < 0)
            {
                return ValueCannotBeNull("Check that Name or Price are not null or empty and a list of product ids are available");
            }

            foreach (Guid mProduct in model.ProductIds)
            {
                if (_getProductService.GetProduct(mProduct) == null)
                {
                    return DoesNotExist();
                }
            }
            _updateOrderService.Update(order, model.Name, model.Description, model.Price, model.ProductIds);

            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            if (order == null)
            {
                return DoesNotExist();
            }
            _deleteOrderService.Delete(order);

            return Found();
        }

        [Route("{OrderId:guid}")]
        [HttpGet]
        public HttpResponseMessage GetOrder(Guid orderId)
        {
            var order = _getOrderService.GetOrder(orderId);
            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        //public HttpResponseMessage GetProducts(int skip, int take, ProductTypes? type = null, string name = null)
        public HttpResponseMessage GetOrders(string name = null)
        {
            var orders = _getOrderService.GetOrders(name)
                           .Select(q => new OrderData(q))
                           .ToList();

            return Found(orders);
        }

    }
}