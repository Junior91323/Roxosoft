namespace Roxosoft.BLL.Services.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Roxosoft.BLL.Services.Abstract;
    using Roxosoft.Common;
    using Roxosoft.Common.Models;
    using Roxosoft.DAL.Repositories.Abstract;

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public async Task<ProductModel> GetByUid(Guid uid)
        {
            return await _productRepository.GetByUid(uid);
        }

        public async Task<Tuple<IList<ProductModel>, int>> GetList(PageSortInfo pageSort)
        {
            return await _productRepository.GetList(pageSort);
        }

        public async Task<Tuple<IList<ProductModel>, int>> GetList(string terms, PageSortInfo pageSort)
        {
            Tuple<IList<ProductModel>, int> list;

            if (!string.IsNullOrWhiteSpace(terms))
            {
                list = await _productRepository.GetList((x => x.Description.Contains(terms)), pageSort);
            }
            else
            {
                list = await _productRepository.GetList(pageSort);
            }
            return list;
        }

        public IQueryable<ProductModel> Find(Expression<Func<ProductModel, bool>> predicate)
        {
            return _productRepository.Find(predicate);
        }

        public async Task Create(ProductModel model, Guid? userUid)
        {

            if (model == null)
                throw new NullReferenceException();

            if (model.Uid == null || model.Uid == Guid.Empty)
                model.Uid = Guid.NewGuid();

            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.CreateUserUid = userUid;
            model.IsActive = true;

            await _productRepository.Create(model);
            await _productRepository.Save();
        }

        public async Task Update(ProductModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;
            model.UpdateUserUid = userUid;

            _productRepository.Update(model);
            await _productRepository.Save();
        }

        public async Task Delete(ProductModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;
            model.UpdateUserUid = userUid;
            model.IsActive = false;

            _productRepository.Delete(model);
            await _productRepository.Save();
        }
    }
}
