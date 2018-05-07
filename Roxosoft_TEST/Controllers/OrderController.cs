using Microsoft.AspNetCore.Mvc;
using Roxosoft.BLL.Services.Abstract;
using Roxosoft.Common;
using Roxosoft.Common.Enums;
using Roxosoft.Common.Models;
using Roxosoft_TEST.Mappers;
using Roxosoft_TEST.Models;
using Roxosoft_TEST.Models.Cart;
using Roxosoft_TEST.Models.Orders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roxosoft_TEST.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderService.GetById(id);

            var res = OrderMapper.Map(item);

            return PartialView("OrderInfo", res);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _orderService.GetList(null, PageSortInfo.All);

            var res = list.Item1.Select(OrderMapper.Map).ToArray();

            return View(res);
        }

        [HttpPost]
        public async Task<ResultInfo> Create(OrderRequestModel request)
        {
            if (!ModelState.IsValid)
                return new ResultInfo(ModelState.Values.ToString(), "403");

            var model = new OrderModel();

            OrderMapper.Map(request, model);

            await _orderService.Create(model, null);

            return new ResultInfo();
        }

        [HttpPut]
        public async Task<ResultInfo> Complete(int id)
        {
            var model = await _orderService.GetById(id);

            if (model == null)
                return new ResultInfo($"Order #{id} not found ", "404");

            if (model.Status == (int)OrderStatus.Complete)
                return new ResultInfo($"Order #{id} is already complete!", "403");

            await _orderService.Complete(model, null);

            return new ResultInfo();
        }
    }
}