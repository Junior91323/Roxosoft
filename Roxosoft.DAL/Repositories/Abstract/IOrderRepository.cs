namespace Roxosoft.DAL.Repositories.Abstract
{
    using System.Threading.Tasks;
    using Roxosoft.Common.Models;

    public interface IOrderRepository : IBaseRepository<OrderModel>
    {
        Task<OrderModel> GetById(int id);
    }
}
