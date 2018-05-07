namespace Roxosoft_TEST.Models.Orders
{
    using System;
    using Roxosoft.Common.Enums;
    using Roxosoft_TEST.Models.Product;

    public class OrderViewModel
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public string Description { get; set; }

        public DateTime CreateDate { get; set; }

        public ProductViewModel[] Products { get; set; }
    }
}
