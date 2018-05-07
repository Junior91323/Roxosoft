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

    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartService _cartService;


        public OrderService(IOrderRepository orderRepository, ICartService cartService)
        {
            this._orderRepository = orderRepository;
            this._cartService = cartService;
        }

        public async Task<OrderModel> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }

        public async Task<Tuple<IList<OrderModel>, int>> GetList(PageSortInfo pageSort)
        {
            return await _orderRepository.GetList(pageSort);
        }

        public async Task<Tuple<IList<OrderModel>, int>> GetList(string terms, PageSortInfo pageSort)
        {
            Tuple<IList<OrderModel>, int> list;

            if (!string.IsNullOrWhiteSpace(terms))
            {
                list = await _orderRepository.GetList((x => x.Description.Contains(terms)), pageSort);
            }
            else
            {
                list = await _orderRepository.GetList(pageSort);
            }
            return list;
        }

        public IQueryable<OrderModel> Find(Expression<Func<OrderModel, bool>> predicate)
        {
            return _orderRepository.Find(predicate);
        }

        public async Task Create(OrderModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            var cartProducts = await _cartService.GetList(PageSortInfo.All);

            foreach (var prod in cartProducts.Item1)
            {
                model.ProductsInOrders.Add(new ProductsInOrders() { ProductCount = prod.ProductCount, ProductUid = prod.ProductUid });
            }

            model.TotalCount = cartProducts.Item1.Sum(x => x.ProductCount);
            model.TotalPrice = cartProducts.Item1.Sum(x => x.Product.Price * x.ProductCount);
            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.CreateUserUid = userUid;
            model.Status = (int)OrderStatus.InProgress;
            model.IsActive = true;

            await _orderRepository.Create(model);
            await _orderRepository.Save();

            _cartService.Clear();
        }

        public async Task Update(OrderModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;
            model.UpdateUserUid = userUid;

            _orderRepository.Update(model);
            await _orderRepository.Save();
        }

        public async Task Complete(OrderModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;
            model.UpdateUserUid = userUid;
            model.Status = (int)OrderStatus.Complete;

            _orderRepository.Update(model);
            await _orderRepository.Save();
        }

        public async Task Delete(OrderModel model, Guid? userUid)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;
            model.UpdateUserUid = userUid;
            model.IsActive = false;

            _orderRepository.Delete(model);
            await _orderRepository.Save();
        }
    }
}
