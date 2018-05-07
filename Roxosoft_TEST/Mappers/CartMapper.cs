namespace Roxosoft_TEST.Mappers
{
    using System;
    using Roxosoft.Common.Models;
    using Roxosoft_TEST.Models.Cart;

    internal static class CartMapper
    {
        internal static CartViewModel Map(CartModel model)
        {
            return new CartViewModel
            {
                ProductCount = model.ProductCount,
                Product = ProductMapper.Map(model.Product)
            };
        }

        internal static void Map(CartRequestModel item, CartModel model)
        {
            model.ProductUid = item.ProductUid;
            model.ProductCount = item.Count;
        }
    }
}
