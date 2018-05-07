namespace Roxosoft.BLL.Services.Abstract
{
    using System;
    using System.Threading.Tasks;
    using Roxosoft.Common.Models;

    public interface IProductService : IBaseService<ProductModel>
    {
        Task<ProductModel> GetByUid(Guid uid);
    }
}
