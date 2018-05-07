namespace Roxosoft.DAL.Repositories.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Roxosoft.Common;
    using Roxosoft.Common.Extensions;
    using Roxosoft.Common.Models;
    using Roxosoft.DAL.Repositories.Abstract;

    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _db;

        public OrderRepository(DBContext context)
        {
            this._db = context;
        }

        public async Task<OrderModel> GetById(int id)
        {
            return await _db.Orders.Include(x => x.ProductsInOrders).ThenInclude(x => x.Product).Where(x => x.Id == id && x.IsActive).FirstOrDefaultAsync();
        }

        public async Task<Tuple<IList<OrderModel>, int>> GetList(PageSortInfo pageSort)
        {
            var totalCount = await _db.Orders.Where(x => x.IsActive).CountAsync();

            var list = await _db.Orders.Where(x => x.IsActive).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<OrderModel>, int>(list, totalCount);
        }

        public async Task<Tuple<IList<OrderModel>, int>> GetList(Expression<Func<OrderModel, bool>> predicate, PageSortInfo pageSort)
        {
            var totalCount = await _db.Orders.Where(predicate).Where(x => x.IsActive).CountAsync();

            var list = await _db.Orders.Where(x => x.IsActive).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<OrderModel>, int>(list, totalCount);
        }

        public IQueryable<OrderModel> Find(Expression<Func<OrderModel, bool>> predicate)
        {
            return _db.Orders.Where(predicate);
        }

        public async Task Create(OrderModel model)
        {
            await _db.Orders.AddAsync(model);
        }

        public void Update(OrderModel model)
        {
            //db.Articles.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
        }

        public void Delete(OrderModel model)
        {
            //db.Articles.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}
