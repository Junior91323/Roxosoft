namespace Roxosoft.DAL.Repositories.Abstract
{
    using System;
    using System.Threading.Tasks;
    using Roxosoft.Common.Models;

    public interface IProductRepository : IBaseRepository<ProductModel>
    {
        Task<ProductModel> GetByUid(Guid uid);
    }
}
