namespace Roxosoft_TEST.Mappers
{
    using System;
    using System.Linq;
    using Roxosoft.Common.Enums;
    using Roxosoft.Common.Models;
    using Roxosoft_TEST.Models.Orders;
    using Roxosoft_TEST.Models.Product;

    internal static class OrderMapper
    {
        internal static OrderViewModel Map(OrderModel model)
        {
            return new OrderViewModel
            {
                Id = model.Id,
                CreateDate = model.CreateDate,
                Status = (OrderStatus)model.Status,
                TotalCount = model.TotalCount,
                TotalPrice = model.TotalPrice,
                Description = model.Description,
                Products = model.ProductsInOrders.Select(x => new ProductViewModel() { Uid = x.ProductUid, Name = x.Product.Name, Price = x.Product.Price, Qty = x.ProductCount }).ToArray()
            };
        }

        internal static void Map(OrderRequestModel item, OrderModel model)
        {
            model.Description = item.Description;
        }
    }
}
