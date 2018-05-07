namespace Roxosoft_TEST.Mappers
{
    using System;
    using Roxosoft.Common.Models;
    using Roxosoft_TEST.Models.Product;

    internal static class ProductMapper
    {
        internal static ProductViewModel Map(ProductModel model)
        {
            return new ProductViewModel
            {
                Uid = model.Uid,
                Name = model.Name,
                Price = model.Price
            };
        }

        internal static void Map(ProductRequestModel item, ProductModel model)
        {
            model.Price = item.Price;
            model.Name = item.Name;
        }
    }
}
