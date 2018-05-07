namespace Roxosoft.Common.Models
{
    using System;

    public class ProductsInOrders
    {
        public Guid ProductUid { get; set; }
        public ProductModel Product { get; set; }

        public int OrderId { get; set; }
        public OrderModel Order { get; set; }

        public int ProductCount { get; set; }
    }
}
