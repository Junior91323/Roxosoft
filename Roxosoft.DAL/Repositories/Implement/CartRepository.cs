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

    public class CartRepository : ICartRepository
    {
        private readonly DBContext _db;

        public CartRepository(DBContext context)
        {
            this._db = context;
        }

        public async Task<CartModel> GetById(int id)
        {
            return await _db.Cart.Include(x => x.Product).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<CartModel> GetByProductUid(Guid uid)
        {
            return await _db.Cart.Include(x => x.Product).Where(x => x.ProductUid == uid).FirstOrDefaultAsync();
        }

        public async Task<Tuple<IList<CartModel>, int>> GetList(PageSortInfo pageSort)
        {
            var totalCount = await _db.Cart.CountAsync();

            var list = await _db.Cart.Include(x => x.Product).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<CartModel>, int>(list, totalCount);
        }

        public async Task<Tuple<IList<CartModel>, int>> GetList(Expression<Func<CartModel, bool>> predicate, PageSortInfo pageSort)
        {
            var totalCount = await _db.Cart.Where(predicate).CountAsync();

            var list = await _db.Cart.Include(x => x.Product).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<CartModel>, int>(list, totalCount);
        }

        public async Task Create(CartModel model)
        {
            await _db.Cart.AddAsync(model);
        }

        public void Delete(CartModel model)
        {
            _db.Cart.Remove(model);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public void Clear()
        {
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Cart]");
        }

        public IQueryable<CartModel> Find(Expression<Func<CartModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Update(CartModel model)
        {
            _db.Entry(model).State = EntityState.Modified;
        }
    }
}
