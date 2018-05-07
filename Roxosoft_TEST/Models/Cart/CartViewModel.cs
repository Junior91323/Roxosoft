namespace Roxosoft_TEST.Models.Cart
{
    using Roxosoft_TEST.Models.Product;

    public class CartViewModel
    {
        public int ProductCount { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
