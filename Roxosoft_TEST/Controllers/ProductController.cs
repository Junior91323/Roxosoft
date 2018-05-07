using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Roxosoft.BLL.Services.Abstract;
using Roxosoft.Common;
using Roxosoft.Common.Models;
using Roxosoft_TEST.Mappers;
using Roxosoft_TEST.Models.Product;

namespace Roxosoft_TEST.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var list = await _productService.GetList(null, PageSortInfo.All);

            var res = list.Item1.Select(ProductMapper.Map).ToArray();

            return View(res);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var model = new ProductModel();

            ProductMapper.Map(request, model);

            await _productService.Create(model, null);

            return RedirectToAction("List");
        }
    }
}