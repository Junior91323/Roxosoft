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

    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _db;

        public ProductRepository(DBContext context)
        {
            this._db = context;
        }

        public async Task<ProductModel> GetByUid(Guid uid)
        {
            return await _db.Products.Where(x => x.Uid == uid && x.IsActive).FirstOrDefaultAsync();
        }

        public async Task<Tuple<IList<ProductModel>, int>> GetList(PageSortInfo pageSort)
        {
            var totalCount = await _db.Products.Where(x => x.IsActive).CountAsync();

            var list = await _db.Products.Where(x => x.IsActive).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<ProductModel>, int>(list, totalCount);
        }

        public async Task<Tuple<IList<ProductModel>, int>> GetList(Expression<Func<ProductModel, bool>> predicate, PageSortInfo pageSort)
        {
            var totalCount = await _db.Products.Where(predicate).Where(x => x.IsActive).CountAsync();

            var list = await _db.Products.Where(x => x.IsActive).Sort(pageSort).Skip(pageSort.PageIndex * pageSort.PageSize).Take(pageSort.PageSize).ToListAsync();

            return new Tuple<IList<ProductModel>, int>(list, totalCount);
        }

        public IQueryable<ProductModel> Find(Expression<Func<ProductModel, bool>> predicate)
        {
            return _db.Products.Where(predicate);
        }

        public async Task Create(ProductModel model)
        {
            await _db.Products.AddAsync(model);
        }

        public void Update(ProductModel model)
        {
            //db.Articles.Attach(model);
            _db.Entry(model).State = EntityState.Modified;
        }

        public void Delete(ProductModel model)
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
