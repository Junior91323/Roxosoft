namespace Roxosoft.BLL.Services.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Roxosoft.BLL.Services.Abstract;
    using Roxosoft.Common;
    using Roxosoft.Common.Enums;
    using Roxosoft.Common.Models;
    using Roxosoft.DAL.Repositories.Abstract;

    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }

        public async Task<CartModel> GetById(int id)
        {
            return await _cartRepository.GetById(id);
        }

        public async Task<CartModel> GetByProductUid(Guid uid)
        {
            return await _cartRepository.GetByProductUid(uid);
        }

        public async Task<Tuple<IList<CartModel>, int>> GetList(string terms, PageSortInfo pageSort)
        {
            Tuple<IList<CartModel>, int> list;

            if (!string.IsNullOrWhiteSpace(terms))
            {
                list = await _cartRepository.GetList((x => x.Product.Name.Contains(terms)), pageSort);
            }
            else
            {
                list = await _cartRepository.GetList(pageSort);
            }
            return list;
        }

        public async Task Create(CartModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            await _cartRepository.Create(model);
            await _cartRepository.Save();
        }

        public void Clear()
        {
            _cartRepository.Clear();
            _cartRepository.Save();
        }

        public async Task<Tuple<IList<CartModel>, int>> GetList(PageSortInfo pageSort)
        {
            return await _cartRepository.GetList(pageSort);
        }

        public IQueryable<CartModel> Find(Expression<Func<CartModel, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Update(CartModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            _cartRepository.Update(model);
            await _cartRepository.Save();
        }

        public async Task Delete(CartModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            _cartRepository.Delete(model);
            await _cartRepository.Save();
        }
    }
}
