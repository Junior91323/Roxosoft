namespace Roxosoft_TEST.Models.Product
{
    using System;

    public class ProductViewModel
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }
    }
}
