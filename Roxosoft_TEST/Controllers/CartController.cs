using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roxosoft.BLL.Services.Abstract;
using Roxosoft.Common;
using Roxosoft.Common.Models;
using Roxosoft_TEST.Mappers;
using Roxosoft_TEST.Models;
using Roxosoft_TEST.Models.Cart;

namespace Roxosoft_TEST.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IProductService productService)
        {
            this._cartService = cartService;
            this._productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _cartService.GetList(null, PageSortInfo.All);

            var res = list.Item1.Select(CartMapper.Map).ToArray();

            return PartialView("Cart", res);
        }

        [HttpPost]
        public async Task<ResultInfo> Create([FromBody]CartRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                    foreach (var modelError in modelState.Errors)
                        modelErrors.Add(modelError.ErrorMessage);

                return new ResultInfo(String.Join(' ', modelErrors), "403");
            }

            var model = await _cartService.GetByProductUid(request.ProductUid);

            if (model == null)
            {
                model = new CartModel();

                CartMapper.Map(request, model);

                await _cartService.Create(model, null);
            }
            else
            {
                model.ProductCount += request.Count;

                await _cartService.Update(model, null);
            }

            return new ResultInfo();
        }

        [HttpDelete]
        public async Task<ResultInfo> Delete(Guid uid)
        {
            var model = await _cartService.GetByProductUid(uid);

            if (model == null)
                new ResultInfo($"Item not found {uid}", "404");

            await _cartService.Delete(model, null);

            return new ResultInfo();
        }
    }
}