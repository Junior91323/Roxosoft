namespace Roxosoft.DAL.Repositories.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Roxosoft.Common;
    using Roxosoft.Common.Models;

    public interface ICartRepository : IBaseRepository<CartModel>
    {
        Task<CartModel> GetById(int id);

        Task<CartModel> GetByProductUid(Guid uid);

        void Clear();
    }
}
