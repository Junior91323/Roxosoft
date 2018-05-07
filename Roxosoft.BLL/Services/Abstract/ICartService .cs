namespace Roxosoft.BLL.Services.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Roxosoft.Common;
    using Roxosoft.Common.Models;

    public interface ICartService : IBaseService<CartModel>
    {
        Task<CartModel> GetById(int id);

        Task<CartModel> GetByProductUid(Guid uid);

        void Clear();
    }
}
