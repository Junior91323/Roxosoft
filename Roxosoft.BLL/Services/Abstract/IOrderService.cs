namespace Roxosoft.BLL.Services.Abstract
{
    using System;
    using System.Threading.Tasks;
    using Roxosoft.Common.Models;

    public interface IOrderService : IBaseService<OrderModel>
    {
        Task<OrderModel> GetById(int id);

        Task Complete(OrderModel model, Guid? userUid);
    }
}
